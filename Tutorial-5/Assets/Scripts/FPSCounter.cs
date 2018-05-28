using UnityEngine;

public class FPSCounter : MonoBehaviour {

    public int AverageFPS { get; private set; }

    public int frameRange = 60;

    int[] fpsBuffer;
    int fpsBufferIndex;

    private void Update()
    {
        if (fpsBuffer == null || fpsBuffer.Length != frameRange) {
            InitializeBuffer();
        }
        UpdateBuffer();
        CalculateFPS();
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

    // method to update the fpsBuffer and discard the oldest values in the buffer
    void UpdateBuffer()
    {
        fpsBuffer[fpsBufferIndex + 1] = (int) (1f / Time.unscaledDeltaTime);
        
        if (fpsBufferIndex >= frameRange) {
            fpsBufferIndex = 0; // discard the oldest value in the buffer
        }
    }

    // method that calculates average fps
    void CalculateFPS()
    {
        int sum = 0;
        for (int i = 0; i< frameRange; i++)
        {
            sum += fpsBuffer[i];
        }
        AverageFPS = sum / frameRange;
    }
}
