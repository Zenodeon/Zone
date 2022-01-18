using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public class ThumbnailExtactorManager 
{
    public static ThumbnailExtactorManager _instance;

    private ThumbnailExtactor extactor;

    private int maxSFECount = 8;
    private int maxMFECount = 1;

    Queue<int> availableSFExtractor = new Queue<int>();
    Queue<int> availableMFExtractor = new Queue<int>();

    //Queue<SingleFrameRequest> singleFrameRequests = new Queue<SingleFrameRequest>();
    Queue<MultiFrameRequest> multiFrameRequests = new Queue<MultiFrameRequest>();

    public void Instantiate()
    {
        _instance = this;
        extactor = new ThumbnailExtactor();

        for (int i = 0; i < maxMFECount; i++)
            availableMFExtractor.Enqueue(i);
    }

    public void test()
    {
        DebugLogger.Wpf.DLog.Log("works");
    }

    //public void GetThumbnail(string url, ulong singleFrameIndex, Action<Texture> singleFrameCallback)
    //{
    //    SingleFrameRequest singleFrameRequest = new SingleFrameRequest(url, singleFrameIndex, singleFrameCallback, DequeueSFE);
    //    ProcessRequest(singleFrameRequest);
    //}

    public void GetThumbnailPreview(string url, Action<BitmapImage> multiFrameCallBack)
    {
        MultiFrameRequest multiFrameRequest = new MultiFrameRequest(url, multiFrameCallBack, DequeueMFE);
        ProcessRequest(multiFrameRequest);
    }

    //public void GetThumbnails(string url, ulong singleFrameIndex, int multiFrameDuration, Action<Texture> singleFrameCallback, Action<List<GIFFrame>> multiFrameCallBack)
    //{
    //    GetThumbnail(url, singleFrameIndex, singleFrameCallback);
    //    GetThumbnailPreview(url, multiFrameCallBack);
    //}

    //private void ProcessRequest(SingleFrameRequest request, int withID = -1)
    //{
    //    if (withID == -1)
    //    {
    //        if (availableSFExtractor.Count != 0)
    //        {
    //            int id;
    //            id = availableSFExtractor.Dequeue();

    //            request.id = id;
    //            SFExtractorDictionary[id].ExecuteRequest(request);
    //        }
    //        else
    //            singleFrameRequests.Enqueue(request);
    //    }
    //    else
    //    {
    //        request.id = withID;
    //        SFExtractorDictionary[withID].ExecuteRequest(request);
    //    }
    //}

    private void ProcessRequest(MultiFrameRequest request, int withID = -1)
    {
        if (withID == -1)
        {
            if (availableMFExtractor.Count != 0)
            {
                int id;
                id = availableMFExtractor.Dequeue();
                request.id = id;

                extactor.GetFrames(request.url, request.callback);
            }
            else
                multiFrameRequests.Enqueue(request);
        }
        else
        {
            request.id = withID;
            extactor.GetFrames(request.url, request.callback);
        }
    }

    //private void DequeueSFE(int id)
    //{
    //    if (singleFrameRequests.Count == 0)
    //        availableSFExtractor.Enqueue(id);
    //    else
    //        ProcessRequest(singleFrameRequests.Dequeue(), withID: id);
    //}

    private void DequeueMFE(int id)
    {

        if (multiFrameRequests.Count == 0)
            availableMFExtractor.Enqueue(id);
        else
            ProcessRequest(multiFrameRequests.Dequeue(), withID: id);
    }

    //public class SingleFrameRequest
    //{
    //    public int id = -1;

    //    public string url;
    //    public ulong frameIndex;

    //    public Action<Texture> callback;

    //    public SingleFrameRequest(string url, ulong frameIndex,  Action<Texture> callback, Action<int> onComplete)
    //    {
    //        this.url = url;
    //        this.frameIndex = frameIndex;

    //        this.callback = (Texture texture) =>
    //        {
    //            callback.Invoke(texture);
    //            onComplete.Invoke(id);
    //        };
    //    }
    //}

    [Serializable]
    public class MultiFrameRequest
    {
        public int id = -1;

        public string url;

        public Action<BitmapImage> callback;

        public MultiFrameRequest(string url, Action<BitmapImage> callback, Action<int> onComplete)
        {
            this.url = url;
            this.callback = (BitmapImage extractedData) =>
            {
                callback.Invoke(extractedData);
                onComplete.Invoke(id);
            };
        }
    }
}
