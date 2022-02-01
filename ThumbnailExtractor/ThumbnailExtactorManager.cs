using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

public class ThumbnailExtactorManager
{
    public static ThumbnailExtactorManager _instance;

    private ThumbnailExtactor extactor;

    private int maxSFECount = 8;
    private int maxMFECount = 8;

    Queue<int> availableSFExtractor = new Queue<int>();
    Queue<int> availableMFExtractor = new Queue<int>();

    Queue<FrameRequest> singleFrameRequests = new Queue<FrameRequest>();
    Queue<FrameRequest> multiFrameRequests = new Queue<FrameRequest>();

    public void Instantiate()
    {
        _instance = this;
        extactor = new ThumbnailExtactor();

        for (int i = 0; i < maxSFECount; i++)
            availableSFExtractor.Enqueue(i);

        for (int i = 0; i < maxMFECount; i++)
            availableMFExtractor.Enqueue(i);
    }

    public void GetThumbnail(string url, Action<MemoryStream> singleFrameCallback)
    {
        FrameRequest singleFrameRequest = new FrameRequest(url, RequestType.Single, singleFrameCallback, DequeueExtractor);
        ProcessRequest(singleFrameRequest);
    }

    public void GetThumbnailPreview(string url, Action<MemoryStream> multiFrameCallBack)
    {
        FrameRequest multiFrameRequest = new FrameRequest(url, RequestType.Preview, multiFrameCallBack, DequeueExtractor);
        ProcessRequest(multiFrameRequest);
    }

    public void GetThumbnails(string url, Action<MemoryStream> singleFrameCallback, Action<MemoryStream> multiFrameCallBack)
    {
        GetThumbnail(url, singleFrameCallback);
        GetThumbnailPreview(url, multiFrameCallBack);
    }

    private void ProcessRequest(FrameRequest request, int withID = -1)
    {
        if (withID == -1)
        {
            int extractorID = GetAvailableExtractor(request.requestType);

            if (extractorID == -1)
            {
                if (request.requestType == RequestType.Single)
                    singleFrameRequests.Enqueue(request);
                else
                    multiFrameRequests.Enqueue(request);
            }
            else
                SendFrame(request, extractorID);
        }
        else
            SendFrame(request, withID);
    }

    private void SendFrame(FrameRequest request, int extractorID)
    {
        request.id = extractorID;

        if (request.requestType == RequestType.Single)
            extactor.GetFrame(request.url, request.callback);

        if (request.requestType == RequestType.Preview)
            extactor.GetFrames(request.url, request.callback);
    }

    private int GetAvailableExtractor(RequestType type)
    {
        int extractorID = -1;

        if (type == RequestType.Single)
            if (availableSFExtractor.Count != 0)
                extractorID = availableSFExtractor.Dequeue();

        if (type == RequestType.Preview)
            if (availableMFExtractor.Count != 0)
                extractorID = availableMFExtractor.Dequeue();

        return extractorID;
    }

    private void DequeueExtractor(int id, RequestType requestType)
    {
        if (requestType == RequestType.Single)
        {
            if (singleFrameRequests.Count == 0)
                availableSFExtractor.Enqueue(id);
            else
                ProcessRequest(singleFrameRequests.Dequeue(), withID: id);
        }

        if (requestType == RequestType.Preview)
        {
            if (multiFrameRequests.Count == 0)
                availableMFExtractor.Enqueue(id);
            else
                ProcessRequest(multiFrameRequests.Dequeue(), withID: id);
        }
    }

    public class FrameRequest
    {
        public int id = -1;
        public RequestType requestType;

        public string url;

        public Action<MemoryStream> callback;

        public FrameRequest(string url, RequestType requestType, Action<MemoryStream> callback, Action<int, RequestType> onComplete)
        {
            this.url = url;
            this.requestType = requestType;
            this.callback = (MemoryStream thumbnailStream) =>
            {
                callback.Invoke(thumbnailStream);
                onComplete.Invoke(id, requestType);
            };
        }
    }

    public enum RequestType
    {
        Single,
        Preview
    }
}
