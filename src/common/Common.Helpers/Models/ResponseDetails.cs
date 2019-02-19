using Common.Helpers.Resources;
using System;
using System.Collections.Generic;
using static Helpers.Models.CommonEnums;

namespace Helpers.Models
{
   
    public class ResponseDetailsList<T> : ResponseDetailsBase
    {
        public ResponseDetailsList(List<T> t)
        {
            this.ItemsList = t;
            StatusCode = ResponseStatusCode.Success;
        }
        public ResponseDetailsList(List<T> t, int totalItemsCount)
        {
            this.ItemsList = t;
            this.StatusCode = ResponseStatusCode.Success;
            this.TotalItemsCount = totalItemsCount;
        }
        public ResponseDetailsList(ResponseStatusCode statusCode) : base(statusCode)
        {
            this.ItemsList = new List<T>();

        }
        public ResponseDetailsList(ResponseStatusCode statusCode, string detailedReturnMessage) : base(statusCode, detailedReturnMessage)
        {
            this.ItemsList = new List<T>();
        }
        public ResponseDetailsList(Exception ex) : base(ex)
        {
            this.ItemsList = new List<T>();
        }
        public ResponseDetailsList()
        {
            this.ItemsList = new List<T>();
        }
        public T DefaultSearchFilter { get; set; }
        public dynamic CustomSearchFilter { get; set; }

        public List<T> ItemsList { get; set; }

        public int TotalItemsCount { get; set; }

       
    }
    public class ResponseDetails<T> : ResponseDetailsBase
    {
        public ResponseDetails(T t)
        {
            this.DetailsObject = t;
            StatusCode = ResponseStatusCode.Success;
        }
        public ResponseDetails(ResponseStatusCode statusCode) : base(statusCode)
        {
        }
        public ResponseDetails(ResponseStatusCode statusCode, string detailedReturnMessage) : base(statusCode, detailedReturnMessage)
        {
        }
        public ResponseDetails(Exception ex) : base(ex)
        {

        }
        public ResponseDetails()
        {
            this.DetailsObject = default(T);
        }
        public T DetailsObject { get; set; }

        public int TotalItemsCount { get; set; }

    }
    public class ResponseDetails<T1,T2> : ResponseDetailsBase
    {
        public ResponseDetails(T1 t1,T2 t2)
        {
            this.DetailsObject = t1;
            this.SecondDetailsObject = t2;
            StatusCode = ResponseStatusCode.Success;
        }
        public ResponseDetails(ResponseStatusCode statusCode) : base(statusCode)
        {
        }
        public ResponseDetails(ResponseStatusCode statusCode, string detailedReturnMessage) : base(statusCode, detailedReturnMessage)
        {
        }
        public ResponseDetails(Exception ex) : base(ex)
        {

        }
        public ResponseDetails()
        {
            this.DetailsObject = default(T1);
        }
        public T1 DetailsObject { get; set; }
        public T2 SecondDetailsObject { get; set; }

        public int TotalItemsCount { get; set; }

    }
    public class ResponseDetailsBase
    {
        public ResponseDetailsBase()
        {

        }
        public ResponseDetailsBase(ResponseStatusCode statusCode)
        {
            this.StatusCode = statusCode;

        }
        public ResponseDetailsBase(ResponseStatusCode statusCode, string detailedReturnMessage)
        {
            this.StatusCode = statusCode;
            this.DetailedReturnMessage = detailedReturnMessage;
        }
        public ResponseDetailsBase(Exception exc)
        {
            this.StatusCode = ResponseStatusCode.ServerError;
            this.DetailedReturnMessage = exc.Message;
            this.Exception = exc;
        }
        public Exception Exception { get; set; }
        public ResponseStatusCode StatusCode { get; set; }
        public string ReturnMessage
        {
            get
            {
                var eturnMessage = "";
                switch (StatusCode)
                {
                    case ResponseStatusCode.Success:
                        eturnMessage = CommonRes.SuccessResponse;
                        break;
                    case ResponseStatusCode.NotFound:
                        eturnMessage = CommonRes.NotFoundResponse;

                        break;
                    case ResponseStatusCode.ServerError:
                        eturnMessage = CommonRes.ServerErrorResponse;

                        break;
                    case ResponseStatusCode.BusinessError:
                        eturnMessage = CommonRes.BusinessErrorResponse;
                        break;
                    case ResponseStatusCode.AlreadyExist:
                        eturnMessage = CommonRes.AlreadyExistResponse;
                        break;
                    case ResponseStatusCode.FoundMultiples:
                        eturnMessage = CommonRes.FoundMultipleResponse;
                        break;
                    case ResponseStatusCode.NotAuthenticated:
                        eturnMessage = CommonRes.NotAuthenticatedResponse;
                        break;
                    case ResponseStatusCode.RelatedToOtherData:
                        eturnMessage = CommonRes.RelatedToOtherDataResponse;
                        break;
                    default:
                        break;
                }
                return eturnMessage;
            }
        }
        public string DetailedReturnMessage { get; set; }
    }

}
