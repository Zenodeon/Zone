using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using Zone.CustomClass;

public class ThumbnailExtactorManager
{
    public static ThumbnailExtactorManager _instance;

    private ThumbnailExtactor extactor;

    private int maxSFECount = 8;
    private int maxMFECount = 8;

    Queue<int> availableSFExtractor = new Queue<int>();
    Queue<int> availableMFExtractor = new Queue<int>();

    ThreadQueue<FrameRequest> singleFrameRequests = new ThreadQueue<FrameRequest>();
    ThreadQueue<FrameRequest> multiFrameRequests = new ThreadQueue<FrameRequest>();

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
        FrameRequest singleFrameRequest = new FrameRequest(url, RequestType.Single, singleFrameCallback, ProccessNextSFRequest);
        ProcessRequest(singleFrameRequest);
    }

    public void GetThumbnailPreview(string url, Action<MemoryStream> multiFrameCallBack)
    {
        FrameRequest multiFrameRequest = new FrameRequest(url, RequestType.Preview, multiFrameCallBack, ProccessNextMFRequest);
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
            withID = GetAvailableExtractor(request.requestType);

            if (withID == -1)
            {
                switch (request.requestType)
                {
                    case RequestType.Single:
                        singleFrameRequests.Enqueue(request);
                        break;

                    case RequestType.Preview:
                        multiFrameRequests.Enqueue(request);
                        break;
                }
            }
        }

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

    private void ProccessNextSFRequest(int id)
    {
        if (singleFrameRequests.Count != 0)
            ProcessRequest(singleFrameRequests.Dequeue(), withID: id);
        else
            availableSFExtractor.Enqueue(id);
    }

    private void ProccessNextMFRequest(int id)
    {
        if (multiFrameRequests.Count != 0)
            ProcessRequest(multiFrameRequests.Dequeue(), withID: id);
        else
            availableMFExtractor.Enqueue(id);
    }

    public class FrameRequest
    {
        public int id { get; set; } = -1;
        public RequestType requestType { get; set; }

        public string url { get; private set; }

        public Action<MemoryStream> callback { get; private set; }

        public FrameRequest(string url, RequestType requestType, Action<MemoryStream> callback, Action<int> onComplete)
        {
            this.url = url;
            this.requestType = requestType;
            this.callback = (MemoryStream thumbnailStream) =>
            {
                callback.Invoke(thumbnailStream);
                onComplete.Invoke(id);
            };
        }
    }

    public enum RequestType
    {
        Single,
        Preview
    }
}
