using hn.Core.Model;
using hn.DataAccess.Bll;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace hn.Client.Service
{
    [ServiceContract]
    public interface IAPIService
    {
        [OperationContract]

        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string getStock(string token,int t_StartTime,int t_EndTime,int PageSize,int PageIndex,string CPXH);


        [OperationContract]
        [WebGet(UriTemplate = "/Hello/{name}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string Hello(string name);

    }

}
