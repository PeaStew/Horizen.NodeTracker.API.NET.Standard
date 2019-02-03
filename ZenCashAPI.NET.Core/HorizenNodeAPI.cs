using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static ZenCashAPI.NET.Core.HorizenNodeAPIDataModels;

namespace ZenCashAPI.NET.Core
{
    public static class HorizenNodeAPI
    {
        #region API - Public
        /* API Call: /api/srvstats
        * Expected return: {"server":"ts1.na","region":"na","state":"up","nodes":1050,"up":967,"down":41,"inactive":42,"regional":{"total":4293,"up":3948,"down":180,"inactive":161},"global":{"total":12179,"up":11443,"down":404,"inactive":326},"estearn":"0.0213"}
        */
        public static ServerStats GetServerStats(NodeType nodetype, ServerRegion region = ServerRegion.NONE, ServerNumber number = ServerNumber.NONE)
        {
            return JsonConvert.DeserializeObject<ServerStats>(GetQueryJson(nodetype, region, number, APIPublicCallUrlParts.ServerStats));
        }

        /* API Call: /api/srvlist
         * Expected return: {"region":"na","regions":[["na","North America"],["eu","Europe"]],"servers":["ts2.na","ts1.na","ts3.na","ts4.na","ts1.eu","ts2.eu","ts3.eu","ts4.eu"]}
        */
        public static ServerList GetServerList(NodeType nodetype, ServerRegion region = ServerRegion.NONE, ServerNumber number = ServerNumber.NONE)
        {
            return JsonConvert.DeserializeObject<ServerList>(GetQueryJson(nodetype, region, number, APIPublicCallUrlParts.ServerList));
        }

        /* API Call: /api/earnings
         * Expected return: {"stake":42,"zenbtc":0.00402976,"btcusd":7519,"zenusd":30.29976544,"zenearned":41231.42601336,"zenpaidusd":"1249302.54"}
        */
        public static ServerEarnings GetServerEarnings(NodeType nodetype, ServerRegion region = ServerRegion.NONE, ServerNumber number = ServerNumber.NONE)
        {
            return JsonConvert.DeserializeObject<ServerEarnings>(GetQueryJson(nodetype, region, number, APIPublicCallUrlParts.ServerEarnings));
        }

        /* API Call: /api/chal/open
        * Expected return: {"chalOpenCount":[{"server":"ts1.eu","count":3},{"server":"ts1.na","count":18},{"server":"ts2.eu","count":16},{"server":"ts2.na","count":17},{"server":"ts3.eu","count":1},{"server":"ts3.na","count":9},{"server":"ts4.eu","count":2},{"server":"ts4.na","count":6}]}
        */
        public static ServerOpenChallenges GetServerOpenChallenges(NodeType nodetype, ServerRegion region, ServerNumber number)
        {
            return JsonConvert.DeserializeObject<ServerOpenChallenges>(GetQueryJson(nodetype, region, number, APIPublicCallUrlParts.ServerOpenChallenges));
        }

        /* API Call: /api/node/<node_id>/certstatus
        * Expected return: {"valid":true,"msg":"Hostname zzz.f4240.in matches CN zzz.f4240.in","certinfo":{"subject":{"CN":"zzz.f4240.in"},"issuer":{"C":"US","O":"Let's Encrypt","CN":"Let's Encrypt Authority X3"}},"checked":"2018-05-31T21:21:56.987Z","trynext":false}            
        */
        public static NodeCertStatus GetNodeCertStatus(NodeType nodetype, int nodeid, ServerRegion region = ServerRegion.NONE, ServerNumber number = ServerNumber.NONE)
        {
            return JsonConvert.DeserializeObject<NodeCertStatus>(GetQueryJson(nodetype, region, number, APIPublicCallUrlParts.NodeCertStatus(nodeid)));
        }
        #endregion
        #region Nodes - API  - Non-Paged Requests

        /* Path: /api/nodes/<nodeid>/detail?key=<apikey>
        * Return: returns node details for given nodeid.  If the email of the API key matches the email of the node, the node t-address and stake t-address are also returned as 'taddr' and 'stkaddr'. Any open Exceptions and Downtimes are also returned.
        * Expected return: {"id":9,"status":"up","home":"ts1-testnet.na","curserver":"ts1-testnet.na","ip4":"198.58.105.60","ip6":null,"fqdn":"zen1.secnodes.com","config":{"hw":{"CPU":"Intel(R) Xeon(R) CPU E5-2697 v4 @ 2.30GHz","cores":1,"speed":2299},"node":{"version":2001050,"wallet.version":60000,"protocolversion":170002},"trkver":"0.2.1"},"createdAt":"2017-10-16T19:24:11.000Z","updatedAt":"2018-06-13T22:38:25.000Z","taddr":"ztTYagRWzHZuiZxFTuhTgUZigFyoHNBe65s","stkaddr":"ztaj8xNwDjkLxMYWHnWLCvaxHYLmDQzwcxZ","email":"myemail@gmail.com","exceptions":[{"id":40184,"etype":"chalmax","start":"2018-05-02T17:06:40.000Z","check":"2018-06-13T22:40:22.000Z","end":null}],"hasException":true}
        */
        public static NodeDetail GetNodeDetail(APIKey apikey, string category = null, int nodeid = 0, ServerRegion region = ServerRegion.NONE, ServerNumber number = ServerNumber.NONE)
        {
            return JsonConvert.DeserializeObject<NodeDetail>(GetQueryJson(apikey.APINodeType, region, number, APINonPagedCallUrlParts.NodeDetail(apikey, nodeid)));
        }


        /* Path: /api/nodes/my/list?key=<apikey>
        * Optional Parameters:  &status=<status>  where <status> can be 'up' or 'down'. e.g. &status=up
        * Return: array of nodes associated with the node email address linked to the API key. 
        * Expected return: {"id":9,"status":"up","home":"ts1-testnet.na","curserver":"ts1-testnet.na","ip4":"198.58.105.60","ip6":null,"fqdn":"zen1.secnodes.com","config":{"hw":{"CPU":"Intel(R) Xeon(R) CPU E5-2697 v4 @ 2.30GHz","cores":1,"speed":2299},"node":{"version":2001050,"wallet.version":60000,"protocolversion":170002},"trkver":"0.2.1"},"createdAt":"2017-10-16T19:24:11.000Z","updatedAt":"2018-06-13T22:38:25.000Z","taddr":"ztTYagRWzHZuiZxFTuhTgUZigFyoHNBe65s","stkaddr":"ztaj8xNwDjkLxMYWHnWLCvaxHYLmDQzwcxZ","email":"myemail@gmail.com","exceptions":[{"id":40184,"etype":"chalmax","start":"2018-05-02T17:06:40.000Z","check":"2018-06-13T22:40:22.000Z","end":null}],"hasException":true}
        */
        public static MyNodes GetMyNodes(APIKey apikey, string category = null, ServerRegion region = ServerRegion.NONE, ServerNumber number = ServerNumber.NONE, NodeStatus nodestatus = NodeStatus.NONE)
        {
            return JsonConvert.DeserializeObject<MyNodes>(GetQueryJson(apikey.APINodeType, region, number, APINonPagedCallUrlParts.MyNodes(apikey, nodestatus)));
        }

        /* Path: /api/nodes/my/earnings?key=<apikey>
        * Optional Parameters:   &nid=<nodeid>  return only for specified node. e.g. &nid=435
        * Return: array of nodes with their associated earnings to date along with a record count and summary data that include the total zen earned to date and the current price of Zen in USD. Data is associated with the node email address linked to the API key. 
        * Expected return: {"records":2,"rows":[{"nid":9,"fqdn":"zen1.secnodes.com","zen":"484.41397152","added":"2017-10-16T19:24:11.000Z"},{"nid":12,"fqdn":"zen102.secnodes.com","zen":"5.22053245","added":"2017-10-16T19:25:06.000Z"}],"summary":{"totalzen":489.63450397,"zenusd":17.18517909073966}}
        */
        public static MyEarnings GetMyEarnings(APIKey apikey, int nodeid = 0, string category = null, ServerRegion region = ServerRegion.NONE, ServerNumber number = ServerNumber.NONE)
        {
            return JsonConvert.DeserializeObject<MyEarnings>(GetQueryJson(apikey.APINodeType, region, number, APINonPagedCallUrlParts.MyEarnings(apikey, nodeid)));
        }
        #endregion

        #region Nodes - API  - Paged Requests

        /* Path: /api/nodes/my/downtimes?key=<apikey>&page=<pagenumber>&rows=<rowcount>
        * Optional Parameters:  &nid=<nodeid>  return only for specified node. e.g. &nid=435, &status=<status>  where <status> is 'o' for open or 'c' for closed.  e.g. &status=o
        * Return: Downtimes for all nodes associated with the API key.
        * Expected return: {"page":1,"total":79,"rowsperpage":"10","records":781,"rows":[{"id":120679,"fqdn":"zen1.secnodes.com","home":"ts1-testnet.na","curserver":"ts1-testnet.na","start":"2018-06-12T06:06:20.000Z","check":"2018-06-12T06:19:14.000Z","end":"2018-06-12T06:19:29.000Z","duration":789000,"dtype":"sys","nid":9}]}
        */
        public static MyDowntimes GetMyDowntimes(APIKey apikey, int pagenumber, int rowcount, int nodeid = 0, string category = null, DowntimeStatus downtimestatus = DowntimeStatus.NONE, ServerRegion region = ServerRegion.NONE, ServerNumber number = ServerNumber.NONE)
        {
            return JsonConvert.DeserializeObject<MyDowntimes>(GetQueryJson(apikey.APINodeType, region, number, APIPagedCallUrlParts.MyDowntimes(apikey, pagenumber, rowcount, nodeid, downtimestatus)));
        }

        /* Path: /api/nodes/my/exceptions?key=<apikey>&page=<pagenumber>&rows=<rowcount>
        * Optional Parameters:  &nid=<nodeid>  return only for specified node.  e.g. &nid=435,  &status=<status>  where <status> is 'o' for open or 'c' for closed.   e.g. &status=o
        * Return: Exceptions for all nodes associated with the API key.
        * Expected return: {"page":1,"total":1,"rowsperpage":"10","records":8,"rows":[{"id":40184,"fqdn":"zen1.secnodes.com","home":"ts1-testnet.na","start":"2018-05-02T17:06:40.000Z","check":"2018-06-13T22:03:22.000Z","end":null,"duration":3646602000,"etype":"chalmax","nid":9}]}
        */
        public static MyExceptions GetMyExceptions(APIKey apikey, int pagenumber, int rowcount, int nodeid = 0, string category = null, ExceptionStatus exceptionstatus = ExceptionStatus.NONE, ServerRegion region = ServerRegion.NONE, ServerNumber number = ServerNumber.NONE)
        {
            return JsonConvert.DeserializeObject<MyExceptions>(GetQueryJson(apikey.APINodeType, region, number, APIPagedCallUrlParts.MyExceptions(apikey, pagenumber, rowcount, nodeid, exceptionstatus)));
        }

        /* Path: /api/nodes/my/challenges?key=<apikey>&page=<pagenumber>&rows=<rowcount>
        * Optional Parameters:   &nid=<nodeid>  return only for specified node.  e.g. &nid=435, &result=<result>  where <result> is 'pass' or 'fail'.   e.g. &result=fail
        * Return: Challenge results for all nodes associated with the API key.
        * Expected return: {"page":1,"total":320,"rowsperpage":"10","records":3199,"rows":[{"id":333694,"fqdn":"zen1.secnodes.com","home":"ts1-testnet.na","nid":9,"start":"2018-06-13T21:53:23.000Z","received":null,"reply":null,"run":null,"result":"wait","reason":null}]}
        */
        public static MyChallenges GetMyChallenges(APIKey apikey, int pagenumber, int rowcount, int nodeid = 0, string category = null, ChallengeResult challengestatus = ChallengeResult.NONE, ServerRegion region = ServerRegion.NONE, ServerNumber number = ServerNumber.NONE)
        {
            return JsonConvert.DeserializeObject<MyChallenges>(GetQueryJson(apikey.APINodeType, region, number, APIPagedCallUrlParts.MyChallenges(apikey, pagenumber, rowcount, nodeid, challengestatus)));
        }

        /* Path: /api/nodes/my/payments?key=<apikey>&page=<pagenumber>&rows=<rowcount>
        * Optional Parameters:    &nid=<nodeid>  return only for specified node.  e.g. &nid=435,  &status=<status>  where <status> is 'exclude'  e.g. &status=exclude
        * Return: Payments and Credits for all nodes associated with the API key. The type is 'e' for earnings and 'c' for credit
        * Expected return: {"page":61,"total":91,"rowsperpage":"10","records":903,"rows":[{"id":243031,"status":"exclude","startdate":"2017-12-13T12:32:32.000Z","enddate":"2017-12-13T18:55:17.000Z","pmid":496,"type":"e","uptime":"0.7774","zen":"0.00000000","created":"2017-12-13T19:24:24.000Z","paidat":null,"txid":null}]}
        */

        public static MyPayments GetMyPayments(APIKey apikey, int pagenumber, int rowcount, int nodeid = 0, string category = null, PaymentStatus paymentstatus = PaymentStatus.NONE, ServerRegion region = ServerRegion.NONE, ServerNumber number = ServerNumber.NONE)
        {
            return JsonConvert.DeserializeObject<MyPayments>(GetQueryJson(apikey.APINodeType, region, number, APIPagedCallUrlParts.MyPayments(apikey, pagenumber, rowcount, nodeid, paymentstatus)));
        }
        #endregion


        private static string GetQueryJson(NodeType nodetype, ServerRegion region, ServerNumber number, string querystring)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var wc = new WebClient())
            {
                if(region != ServerRegion.NONE && number != ServerNumber.NONE) return wc.DownloadString(APIServer.GetServer(nodetype, region, number) + querystring);
                else if (region != ServerRegion.NONE) return wc.DownloadString(APIServer.GetServer(nodetype, region) + querystring);
                else return wc.DownloadString(APIServer.GetServer(nodetype) + querystring);
            }
        }        
    }
}
