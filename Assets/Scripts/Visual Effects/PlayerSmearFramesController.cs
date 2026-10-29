using System.Collections;
using UnityEngine;

public class PlayerSmearFramesController : MonoBehaviour
{
    [SerializeField] GameObject SpriteClonePrefab;
    [SerializeField] PlayerDirectionController DirectionController;
    [SerializeField] SpriteRenderer PlayerSpriteRenderer;
    private float SPAWN_INTERVAL = 0.05f;

    public void SpawnSmearFrames(int spawnCount) => StartCoroutine(SpawnSmearFramesImpl(spawnCount));

    private IEnumerator SpawnSmearFramesImpl(int spawnCount)
    {
        int spawnCounter = 0;
        while (spawnCounter < spawnCount) {
            yield return new WaitForSeconds(SPAWN_INTERVAL);
            StartCoroutine(SpawnSmearFrame(0.25f));
            spawnCounter++;
        }
    }

    private void SetWhiteTextureWithOutline(SpriteRenderer renderer)
    {
        Texture2D originalTexture = renderer.sprite.texture;
        Texture2D newTexture = new Texture2D(originalTexture.width, originalTexture.height);
        newTexture.filterMode = FilterMode.Point;
        Color[] cols = originalTexture.GetPixels();
        for (int i = 0;  i < cols.Length; i++) if (cols[i].a == 1 && cols[i].grayscale >= 0.1f) cols[i] = Color.white;
        newTexture.SetPixels(cols);
        newTexture.Apply();
        Rect spriteRect = new Rect(renderer.sprite.rect.x, renderer.sprite.rect.y, renderer.sprite.rect.width, renderer.sprite.rect.height);
        renderer.sprite = Sprite.Create(newTexture, spriteRect, new Vector2(0.5f, 0.5f), renderer.sprite.pixelsPerUnit);
    }

    private IEnumerator SpawnSmearFrame(float duration)
    {
        GameObject SpriteClone = Instantiate(SpriteClonePrefab, transform.position, Quaternion.identity);
        SpriteRenderer CloneSpriteRenderer = SpriteClone.GetComponent<SpriteRenderer>();
        CloneSpriteRenderer.sprite = PlayerSpriteRenderer.sprite;
        CloneSpriteRenderer.drawMode = PlayerSpriteRenderer.drawMode;
        CloneSpriteRenderer.flipX = PlayerSpriteRenderer.flipX;
        CloneSpriteRenderer.flipY = DirectionController.facingYDirection == 1 ? false : true;
        SetWhiteTextureWithOutline(CloneSpriteRenderer);
        yield return new WaitForSeconds(duration);
        Destroy(SpriteClone);
    }
}
