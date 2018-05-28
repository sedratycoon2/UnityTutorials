using UnityEngine;

public class FPSCounter : MonoBehaviour {

    public int AverageFPS { get; private set; }

    public int frameRange = 60;

    int[] fpsBuffer;
    int fpsBufferIndex;

    private void Update()
    {
        AverageFPS = (int) (1f / Time.unscaledDeltaTime);
    }

    // method that intializes the frame-rate buffer to smooth fps display
    void InitializeBuffer()
    {
        if(frameRange <= 0) {
            frameRange = 1;
        }
        fpsBuffer = new int[frameRange];
        fpsBufferIndex = 0;
    }
}
