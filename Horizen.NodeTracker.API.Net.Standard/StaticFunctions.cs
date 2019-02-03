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

        public static string GetServer(NodeType nodeType, ServerRegion region)
        {
            switch (nodeType)
            {
                case NodeType.SECURE:
                    return "https://securenodes." + GetServerRegion(region) + ".zensystem.io";
                case NodeType.SUPER:
                    return "https://supernodes." + GetServerRegion(region) + ".zensystem.io";
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


        public static string GetServerNumber(ServerRegion serverRegion, int? number)
        {
            if (!number.HasValue) return string.Empty;
            switch (serverRegion)
            {
                case ServerRegion.EU:
                    return number < 7 && number > 0? number.ToString() : string.Empty;
                case ServerRegion.NA:
                    return number < 5 && number > 0 ? number.ToString() : string.Empty;
                default:
                    return string.Empty;
            }
        }

        public static string GetServerRegion(ServerRegion region)
        {
            switch (region)
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
        public static string NodeCertStatus(int nodeid)
        {
            return "/api/node/" + nodeid + "/certstatus";
        }
    }

    public static class APINonPagedCallUrlParts
    {
        public static string NodeDetail(APIKey apikey, int nodeid)
        {
            return "/api/nodes/" + nodeid + "/detail?key=" + apikey.APIkey;
        }

        public static string MyNodes(APIKey apikey, NodeStatus nodestatus = NodeStatus.NONE, string category = null)
        {
            var retval = "/api/nodes/my/list?key=" + apikey.APIkey;
            switch (nodestatus)
            {
                case NodeStatus.UP:
                    retval += "&status=up";
                    break;
                case NodeStatus.DOWN:
                    retval += "&status=down";
                    break;
            }

            if (!string.IsNullOrEmpty(category)) retval += "&cat=" + category;
            return retval;
        }

        public static string MyEarnings(APIKey apikey, int nodeid, string category = null)
        {
            return "/api/nodes/my/list?key=" + apikey + (nodeid > 0 ? "&nid=" + nodeid : "") + (!string.IsNullOrEmpty(category)? "&cat=" + category: "");
        }
    }

    public static class APIPagedCallUrlParts
    {
        public static string MyDowntimes(APIKey apikey, int pagenumber, int rowcount, int nodeid = 0, DowntimeStatus downtimestatus = DowntimeStatus.NONE, string category = null)
        {
            var retval = "/api/nodes/my/downtimes?key=" + apikey.APIkey + "&page=" + pagenumber + "&rows=" + rowcount + (nodeid > 0 ? "&nid=" + nodeid : "") + (!string.IsNullOrEmpty(category) ? "&cat=" + category : "");
            switch (downtimestatus)
            {
                case DowntimeStatus.OPEN:
                    return retval + "&status=o";
                case DowntimeStatus.CLOSED:
                    return retval + "&status=c";
                case DowntimeStatus.EXCLUDE:
                    return retval + "&status=x";
                case DowntimeStatus.NONE:
                default:
                    return retval;
            }
        }

        public static string MyExceptions(APIKey apikey, int pagenumber, int rowcount, int nodeid = 0, ExceptionStatus exceptionstatus = ExceptionStatus.NONE, string category = null)
        {
            var retval = "/api/nodes/my/exceptions?key=" + apikey.APIkey + "&page=" + pagenumber + "&rows=" + rowcount + (nodeid > 0 ? "&nid=" + nodeid : "") + (!string.IsNullOrEmpty(category) ? "&cat=" + category : "");

            switch (exceptionstatus)
            {
                case ExceptionStatus.OPEN:
                    return retval + "&status=o";
                case ExceptionStatus.CLOSED:
                    return retval + "&status=c";
                case ExceptionStatus.EXCLUDE:
                    return retval + "&status=x";
                case ExceptionStatus.NONE:
                default:
                    return retval;
            }
        }

        public static string MyChallenges(APIKey apikey, int pagenumber, int rowcount, int nodeid = 0, ChallengeResult challengeresult = ChallengeResult.NONE, string category = null)
        {
            var retval = "/api/nodes/my/challenges?key=" + apikey.APIkey + "&page=" + pagenumber + "&rows=" + rowcount + (nodeid > 0 ? "&nid=" + nodeid : "") + (!string.IsNullOrEmpty(category) ? "&cat=" + category : "");

            switch (challengeresult)
            {
                case ChallengeResult.PASS:
                    return retval + "&result=pass";
                case ChallengeResult.FAIL:
                    return retval + "&result=fail";
                case ChallengeResult.NONE:
                default:
                    return retval;
            }
        }

        public static string MyPayments(APIKey apikey, int pagenumber, int rowcount, int nodeid = 0, PaymentStatus paymentstatus = PaymentStatus.NONE, string category = null)
        {
            var retval = "/api/nodes/my/Payments?key=" + apikey.APIkey + "&page=" + pagenumber + "&rows=" + rowcount + (nodeid > 0 ? "&nid=" + nodeid : "") + (!string.IsNullOrEmpty(category) ? "&cat=" + category : "");

            switch (paymentstatus)
            {
                case PaymentStatus.EXCLUDE:
                    return retval + "&status=exclude";
                case PaymentStatus.NONE:
                default:
                    return retval;
            }
        }
    }
}

