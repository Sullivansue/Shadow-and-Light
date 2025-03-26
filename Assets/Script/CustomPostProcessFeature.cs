using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomPostProcessFeature : ScriptableRendererFeature
{
    class CustomPostProcessPass : ScriptableRenderPass
    {
        private Material material;
        private RTHandle temporaryColorTexture;
        private RTHandle currentTarget;

        public CustomPostProcessPass(Material material)
        {
            this.material = material;
        }

        public void Setup(RTHandle currentTarget)
        {
            //this.currentTarget = currentTarget;
        }

        // 配置渲染通道
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            // 配置渲染目标和清除参数
            RenderTextureDescriptor descriptor = cameraTextureDescriptor;
            descriptor.depthBufferBits = 0;

            // 分配临时渲染纹理
            temporaryColorTexture = RTHandles.Alloc(descriptor, FilterMode.Bilinear, name: "_TemporaryColorTexture");
        }

        // 执行渲染通道
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (material == null)
            {
                Debug.LogWarning("CustomPostProcessPass 材质丢失");
                return;
            }

            CommandBuffer cmd = CommandBufferPool.Get("Custom Post Processing");

            // 在材质上设置一些参数（如果需要）
            RenderTargetIdentifier cameraColorTexture = renderingData.cameraData.renderer.cameraColorTargetHandle;
            // 将当前渲染结果拷贝到临时渲染纹理并应用后处理材质
            Blit(cmd, cameraColorTexture, temporaryColorTexture, material, 0);

            // 将处理后的结果拷贝回当前渲染目标
            Blit(cmd, temporaryColorTexture, cameraColorTexture);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        // 清理渲染通道
        public override void FrameCleanup(CommandBuffer cmd)
        {
            // 释放临时渲染纹理
            if (temporaryColorTexture != null)
            {
                temporaryColorTexture.Release();
                temporaryColorTexture = null;
            }
        }
    }

    [System.Serializable]
    public class CustomPostProcessSettings
    {
        public Material material = null;
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    }

    public CustomPostProcessSettings settings = new CustomPostProcessSettings();

    CustomPostProcessPass customPostProcessPass;

    public override void Create()
    {
        if (settings.material != null)
        {
            customPostProcessPass = new CustomPostProcessPass(settings.material)
            {
                renderPassEvent = settings.renderPassEvent
            };
        }
    }

    // 添加渲染通道到渲染器
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (customPostProcessPass != null)
        {
            customPostProcessPass.Setup(renderer.cameraColorTargetHandle);
            renderer.EnqueuePass(customPostProcessPass);
        }
    }
}
