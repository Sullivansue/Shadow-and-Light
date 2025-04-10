using System;
using System.Collections;
using System.Collections.Generic;
using Runtime;
using UnityEngine;
using HighlightPlus;
using MoreMountains.Feedbacks;
using Random = UnityEngine.Random;

public class ControlParticlesSpawner : MonoBehaviour {

    public ParticleSystem cps;
    public string bulletTag = "Sword";
    public Shield _shield;
    private Player _player;

    public GameObject lightParticle;

    private void Start()
    {
        //_shield = GameObject.FindGameObjectWithTag("Shield").GetComponent<Shield>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnMouseEnter()
    {
        _player.target = _shield.gameObject;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(bulletTag) && _shield.isHitRightThere)
        {
            //Destroy(collision.gameObject);
            _shield.isHit = true;
            //Instantiate(lightParticle, transform.position, Quaternion.identity);
            cps.transform.position = other.transform.position;
            cps.Emit(1);
            
            
            // 生成光效粒子
            for (int i = 0; i < 2; i++)
            {
                // 生成随机初始旋转
                Quaternion randomRotation = Quaternion.Euler(
                    0,
                    Random.Range(0f, 360f),  // Y轴
                    0
                );
            
                GameObject particle = Instantiate(
                    lightParticle, 
                    transform.position, 
                    randomRotation  // 使用随机旋转
                );
                MMF_Player pPlayer = particle.GetComponent<MMF_Player>();
                pPlayer.PlayFeedbacks();
            }
            
            
        }
    }
}
