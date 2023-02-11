using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{

    public float ghostDelay;
    public SpriteRenderer spriteRShadow;
    SpriteRenderer spriteRPlayer;
    float ghostDelaySeconds;

    // Start is called before the first frame update
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
        spriteRPlayer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ghostDelaySeconds > 0)
        {
            ghostDelaySeconds -= Time.deltaTime;
        } 
        else
        {
            SpriteRenderer currentGhost = Instantiate(spriteRShadow, transform.position, transform.rotation);
            currentGhost.sprite = spriteRPlayer.sprite;
            currentGhost.flipX = Player.player.lookingDir.x < 0;
            ghostDelaySeconds = ghostDelay;
        }
    }
}
