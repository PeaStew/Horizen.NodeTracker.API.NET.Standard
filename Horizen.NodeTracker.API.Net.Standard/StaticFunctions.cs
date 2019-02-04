using System;
using System.Collections.Generic;
using System.Text;
using Horizen.NodeTracker.API.NET.Standard;

namespace Horizen.NodeTracker.API.NET.Standard
{
    public static class APIServer
    {
        public static string GetServer(NodeType nodeType)
        {
            switch (nodeType)
            {
                case NodeType.SECURE:
                    return "https://securenodes.zensystem.io";
                case NodeType.SUPER:
                    return "https://supernodes.zensystem.io";
                default:
                    throw new Exception("Node server type not found");
            }
        }

        public static string GetServer(NodeType nodeType, ServerRegion serverRegion)
        {
            switch (nodeType)
            {
                case NodeType.SECURE:
                    return "https://securenodes." + GetServerRegion(serverRegion) + ".zensystem.io";
                case NodeType.SUPER:
                    return "https://supernodes." + GetServerRegion(serverRegion) + ".zensystem.io";
                default:
                    throw new Exception("Node server type not found");
            }
        }

        public static string GetServer(NodeType nodeType, ServerRegion serverRegion, int? serverNumber)
        {
            var region = GetServerRegion(serverRegion);
            var number = GetServerNumber(serverRegion, serverNumber);
            switch (nodeType)
            {
                case NodeType.SECURE:
                    return "https://securenodes" + number + "." + region + ".zensystem.io";
                case NodeType.SUPER:
                    return "https://supernodes" + number + "." + region + ".zensystem.io";
                default:
                    throw new Exception("Node server type not found");
            }
        }       


        public static string GetServerNumber(ServerRegion serverRegion, int? serverNumber)
        {
            if (!serverNumber.HasValue) return string.Empty;
            switch (serverRegion)
            {
                case ServerRegion.EU:
                    return serverNumber < 7 && serverNumber > 0? serverNumber.ToString() : string.Empty;
                case ServerRegion.NA:
                    return serverNumber < 5 && serverNumber > 0 ? serverNumber.ToString() : string.Empty;
                default:
                    return string.Empty;
            }
        }

        public static string GetServerRegion(ServerRegion serverRegion)
        {
            switch (serverRegion)
            {
                case ServerRegion.EU:
                    return "eu";
                case ServerRegion.NA:
                    return "na";
                case ServerRegion.NONE:
                default:
                    return string.Empty;
            }
        }
    }

    public static class APIPublicCallUrlParts
    {
        public const string ServerStats = "/api/srvstats";
        public const string ServerList = "/api/srvlist";
        public const string ServerEarnings = "/api/earnings";
        public const string ServerOpenChallenges = "/api/chal/open";
        public static string NodeCertStatus(int nodeId)
        {
            return "/api/node/" + nodeId + "/certstatus";
        }
    }

    public static class APINonPagedCallUrlParts
    {
        public static string NodeDetail(APIKey apiKey, int nodeId)
        {
            return "/api/nodes/" + nodeId + "/detail?key=" + apiKey.APIkey;
        }

        public static string MyNodes(APIKey apiKey, NodeStatus nodeStatus = NodeStatus.NONE, string category = null)
        {
            var retVal = "/api/nodes/my/list?key=" + apiKey.APIkey;
            switch (nodeStatus)
            {
                case NodeStatus.UP:
                    retVal += "&status=up";
                    break;
                case NodeStatus.DOWN:
                    retVal += "&status=down";
                    break;
            }

            if (!string.IsNullOrEmpty(category)) retVal += "&cat=" + category;
            return retVal;
        }

        public static string MyEarnings(APIKey apiKey, int nodeId, string category = null)
        {
            return "/api/nodes/my/earnings?key=" + apiKey + (nodeId > 0 ? "&nid=" + nodeId : "") + (!string.IsNullOrEmpty(category)? "&cat=" + category: "");
        }
    }

    public static class APIPagedCallUrlParts
    {
        public static string MyDowntimes(APIKey apiKey, int pageNumber, int rowCount, int nodeId = 0, DowntimeStatus downtimeStatus = DowntimeStatus.NONE, string category = null)
        {
            var retVal = "/api/nodes/my/downtimes?key=" + apiKey.APIkey + "&page=" + pageNumber + "&rows=" + rowCount + (nodeId > 0 ? "&nid=" + nodeId : "") + (!string.IsNullOrEmpty(category) ? "&cat=" + category : "");
            switch (downtimeStatus)
            {
                case DowntimeStatus.OPEN:
                    return retVal + "&status=o";
                case DowntimeStatus.CLOSED:
                    return retVal + "&status=c";
                case DowntimeStatus.EXCLUDE:
                    return retVal + "&status=x";
                case DowntimeStatus.NONE:
                default:
                    return retVal;
            }
        }

        public static string MyExceptions(APIKey apiKey, int pageNumber, int rowCount, int nodeId = 0, ExceptionStatus exceptionStatus = ExceptionStatus.NONE, string category = null)
        {
            var retVal = "/api/nodes/my/exceptions?key=" + apiKey.APIkey + "&page=" + pageNumber + "&rows=" + rowCount + (nodeId > 0 ? "&nid=" + nodeId : "") + (!string.IsNullOrEmpty(category) ? "&cat=" + category : "");

            switch (exceptionStatus)
            {
                case ExceptionStatus.OPEN:
                    return retVal + "&status=o";
                case ExceptionStatus.CLOSED:
                    return retVal + "&status=c";
                case ExceptionStatus.EXCLUDE:
                    return retVal + "&status=x";
                case ExceptionStatus.NONE:
                default:
                    return retVal;
            }
        }

        public static string MyChallenges(APIKey apiKey, int pageNumber, int rowCount, int nodeId = 0, ChallengeResult challengeResult = ChallengeResult.NONE, string category = null)
        {
            var retVal = "/api/nodes/my/challenges?key=" + apiKey.APIkey + "&page=" + pageNumber + "&rows=" + rowCount + (nodeId > 0 ? "&nid=" + nodeId : "") + (!string.IsNullOrEmpty(category) ? "&cat=" + category : "");

            switch (challengeResult)
            {
                case ChallengeResult.PASS:
                    return retVal + "&result=pass";
                case ChallengeResult.FAIL:
                    return retVal + "&result=fail";
                case ChallengeResult.NONE:
                default:
                    return retVal;
            }
        }

        public static string MyPayments(APIKey apiKey, int pageNumber, int rowCount, int nodeId = 0, PaymentStatus paymentStatus = PaymentStatus.NONE, string category = null)
        {
            var retVal = "/api/nodes/my/Payments?key=" + apiKey.APIkey + "&page=" + pageNumber + "&rows=" + rowCount + (nodeId > 0 ? "&nid=" + nodeId : "") + (!string.IsNullOrEmpty(category) ? "&cat=" + category : "");

            switch (paymentStatus)
            {
                case PaymentStatus.EXCLUDE:
                    return retVal + "&status=exclude";
                case PaymentStatus.NONE:
                default:
                    return retVal;
            }
        }
    }
}

