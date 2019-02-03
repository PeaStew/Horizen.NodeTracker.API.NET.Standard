using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Horizen.NodeTracker.API.NET.Standard;
using Newtonsoft.Json;

namespace Horizen.NodeTracker.API.NET.Standard
{
    public static class HorizenNodeAPI
    {
        #region API - Public
        /* API Call: /api/srvstats
        * Expected return: {"server":"ts1.na","serverregion":"na","state":"up","nodes":1050,"up":967,"down":41,"inactive":42,"serverregional":{"total":4293,"up":3948,"down":180,"inactive":161},"global":{"total":12179,"up":11443,"down":404,"inactive":326},"estearn":"0.0213"}
        */
        public static HorizenNodeAPIDataModels.ServerStats GetServerStats(NodeType nodetype, int? servernumber, ServerRegion serverregion = ServerRegion.NONE)
        {
            return JsonConvert.DeserializeObject<HorizenNodeAPIDataModels.ServerStats>(GetQueryJson(nodetype, serverregion, servernumber, APIPublicCallUrlParts.ServerStats));
        }

        /* API Call: /api/srvlist
         * Expected return: {"serverregion":"na","serverregions":[["na","North America"],["eu","Europe"]],"servers":["ts2.na","ts1.na","ts3.na","ts4.na","ts1.eu","ts2.eu","ts3.eu","ts4.eu"]}
        */
        public static HorizenNodeAPIDataModels.ServerList GetServerList(NodeType nodetype, int? servernumber, ServerRegion serverregion = ServerRegion.NONE)
        {
            return JsonConvert.DeserializeObject<HorizenNodeAPIDataModels.ServerList>(GetQueryJson(nodetype, serverregion, servernumber, APIPublicCallUrlParts.ServerList));
        }

        /* API Call: /api/earnings
         * Expected return: {"stake":42,"zenbtc":0.00402976,"btcusd":7519,"zenusd":30.29976544,"zenearned":41231.42601336,"zenpaidusd":"1249302.54"}
        */
        public static HorizenNodeAPIDataModels.ServerEarnings GetServerEarnings(NodeType nodetype, int? servernumber, ServerRegion serverregion = ServerRegion.NONE)
        {
            return JsonConvert.DeserializeObject<HorizenNodeAPIDataModels.ServerEarnings>(GetQueryJson(nodetype, serverregion, servernumber, APIPublicCallUrlParts.ServerEarnings));
        }

        /* API Call: /api/chal/open
        * Expected return: {"chalOpenCount":[{"server":"ts1.eu","count":3},{"server":"ts1.na","count":18},{"server":"ts2.eu","count":16},{"server":"ts2.na","count":17},{"server":"ts3.eu","count":1},{"server":"ts3.na","count":9},{"server":"ts4.eu","count":2},{"server":"ts4.na","count":6}]}
        */
        public static HorizenNodeAPIDataModels.ServerOpenChallenges GetServerOpenChallenges(NodeType nodetype, int? servernumber, ServerRegion serverregion)
        {
            return JsonConvert.DeserializeObject<HorizenNodeAPIDataModels.ServerOpenChallenges>(GetQueryJson(nodetype, serverregion, servernumber, APIPublicCallUrlParts.ServerOpenChallenges));
        }

        /* API Call: /api/node/<node_id>/certstatus
        * Expected return: {"valid":true,"msg":"Hostname zzz.f4240.in matches CN zzz.f4240.in","certinfo":{"subject":{"CN":"zzz.f4240.in"},"issuer":{"C":"US","O":"Let's Encrypt","CN":"Let's Encrypt Authority X3"}},"checked":"2018-05-31T21:21:56.987Z","trynext":false}            
        */
        public static HorizenNodeAPIDataModels.NodeCertStatus GetNodeCertStatus(NodeType nodetype, int nodeid, int? servernumber, ServerRegion serverregion = ServerRegion.NONE)
        {
            return JsonConvert.DeserializeObject<HorizenNodeAPIDataModels.NodeCertStatus>(GetQueryJson(nodetype, serverregion, servernumber, APIPublicCallUrlParts.NodeCertStatus(nodeid)));
        }
        #endregion
        #region Nodes - API  - Non-Paged Requests

        /* Path: /api/nodes/<nodeid>/detail?key=<apikey>
        * Return: returns node details for given nodeid.  If the email of the API key matches the email of the node, the node t-address and stake t-address are also returned as 'taddr' and 'stkaddr'. Any open Exceptions and Downtimes are also returned.
        * Expected return: {"id":9,"status":"up","home":"ts1-testnet.na","curserver":"ts1-testnet.na","ip4":"198.58.105.60","ip6":null,"fqdn":"zen1.secnodes.com","config":{"hw":{"CPU":"Intel(R) Xeon(R) CPU E5-2697 v4 @ 2.30GHz","cores":1,"speed":2299},"node":{"version":2001050,"wallet.version":60000,"protocolversion":170002},"trkver":"0.2.1"},"createdAt":"2017-10-16T19:24:11.000Z","updatedAt":"2018-06-13T22:38:25.000Z","taddr":"ztTYagRWzHZuiZxFTuhTgUZigFyoHNBe65s","stkaddr":"ztaj8xNwDjkLxMYWHnWLCvaxHYLmDQzwcxZ","email":"myemail@gmail.com","exceptions":[{"id":40184,"etype":"chalmax","start":"2018-05-02T17:06:40.000Z","check":"2018-06-13T22:40:22.000Z","end":null}],"hasException":true}
        */
        public static HorizenNodeAPIDataModels.NodeDetail GetNodeDetail(APIKey apikey, int? servernumber, string category = null, int nodeid = 0, ServerRegion serverregion = ServerRegion.NONE)
        {
            return JsonConvert.DeserializeObject<HorizenNodeAPIDataModels.NodeDetail>(GetQueryJson(apikey.APINodeType, serverregion, servernumber, APINonPagedCallUrlParts.NodeDetail(apikey, nodeid)));
        }


        /* Path: /api/nodes/my/list?key=<apikey>
        * Optional Parameters:  &status=<status>  where <status> can be 'up' or 'down'. e.g. &status=up
        * Return: array of nodes associated with the node email address linked to the API key. 
        * Expected return: {"id":9,"status":"up","home":"ts1-testnet.na","curserver":"ts1-testnet.na","ip4":"198.58.105.60","ip6":null,"fqdn":"zen1.secnodes.com","config":{"hw":{"CPU":"Intel(R) Xeon(R) CPU E5-2697 v4 @ 2.30GHz","cores":1,"speed":2299},"node":{"version":2001050,"wallet.version":60000,"protocolversion":170002},"trkver":"0.2.1"},"createdAt":"2017-10-16T19:24:11.000Z","updatedAt":"2018-06-13T22:38:25.000Z","taddr":"ztTYagRWzHZuiZxFTuhTgUZigFyoHNBe65s","stkaddr":"ztaj8xNwDjkLxMYWHnWLCvaxHYLmDQzwcxZ","email":"myemail@gmail.com","exceptions":[{"id":40184,"etype":"chalmax","start":"2018-05-02T17:06:40.000Z","check":"2018-06-13T22:40:22.000Z","end":null}],"hasException":true}
        */
        public static HorizenNodeAPIDataModels.MyNodes GetMyNodes(APIKey apikey, int? servernumber, string category = null, ServerRegion serverregion = ServerRegion.NONE, NodeStatus nodestatus = NodeStatus.NONE)
        {
            return JsonConvert.DeserializeObject<HorizenNodeAPIDataModels.MyNodes>(GetQueryJson(apikey.APINodeType, serverregion, servernumber, APINonPagedCallUrlParts.MyNodes(apikey, nodestatus)));
        }

        /* Path: /api/nodes/my/earnings?key=<apikey>
        * Optional Parameters:   &nid=<nodeid>  return only for specified node. e.g. &nid=435
        * Return: array of nodes with their associated earnings to date along with a record count and summary data that include the total zen earned to date and the current price of Zen in USD. Data is associated with the node email address linked to the API key. 
        * Expected return: {"records":2,"rows":[{"nid":9,"fqdn":"zen1.secnodes.com","zen":"484.41397152","added":"2017-10-16T19:24:11.000Z"},{"nid":12,"fqdn":"zen102.secnodes.com","zen":"5.22053245","added":"2017-10-16T19:25:06.000Z"}],"summary":{"totalzen":489.63450397,"zenusd":17.18517909073966}}
        */
        public static HorizenNodeAPIDataModels.MyEarnings GetMyEarnings(APIKey apikey, int? servernumber, int nodeid = 0, string category = null, ServerRegion serverregion = ServerRegion.NONE)
        {
            return JsonConvert.DeserializeObject<HorizenNodeAPIDataModels.MyEarnings>(GetQueryJson(apikey.APINodeType, serverregion, servernumber, APINonPagedCallUrlParts.MyEarnings(apikey, nodeid)));
        }
        #endregion

        #region Nodes - API  - Paged Requests

        /* Path: /api/nodes/my/downtimes?key=<apikey>&page=<pageservernumber>&rows=<rowcount>
        * Optional Parameters:  &nid=<nodeid>  return only for specified node. e.g. &nid=435, &status=<status>  where <status> is 'o' for open or 'c' for closed.  e.g. &status=o
        * Return: Downtimes for all nodes associated with the API key.
        * Expected return: {"page":1,"total":79,"rowsperpage":"10","records":781,"rows":[{"id":120679,"fqdn":"zen1.secnodes.com","home":"ts1-testnet.na","curserver":"ts1-testnet.na","start":"2018-06-12T06:06:20.000Z","check":"2018-06-12T06:19:14.000Z","end":"2018-06-12T06:19:29.000Z","duration":789000,"dtype":"sys","nid":9}]}
        */
        public static HorizenNodeAPIDataModels.MyDowntimes GetMyDowntimes(APIKey apikey, int pageservernumber, int rowcount, int? servernumber, int nodeid = 0, string category = null, DowntimeStatus downtimestatus = DowntimeStatus.NONE, ServerRegion serverregion = ServerRegion.NONE)
        {
            return JsonConvert.DeserializeObject<HorizenNodeAPIDataModels.MyDowntimes>(GetQueryJson(apikey.APINodeType, serverregion, servernumber, APIPagedCallUrlParts.MyDowntimes(apikey, pageservernumber, rowcount, nodeid, downtimestatus)));
        }

        /* Path: /api/nodes/my/exceptions?key=<apikey>&page=<pageservernumber>&rows=<rowcount>
        * Optional Parameters:  &nid=<nodeid>  return only for specified node.  e.g. &nid=435,  &status=<status>  where <status> is 'o' for open or 'c' for closed.   e.g. &status=o
        * Return: Exceptions for all nodes associated with the API key.
        * Expected return: {"page":1,"total":1,"rowsperpage":"10","records":8,"rows":[{"id":40184,"fqdn":"zen1.secnodes.com","home":"ts1-testnet.na","start":"2018-05-02T17:06:40.000Z","check":"2018-06-13T22:03:22.000Z","end":null,"duration":3646602000,"etype":"chalmax","nid":9}]}
        */
        public static HorizenNodeAPIDataModels.MyExceptions GetMyExceptions(APIKey apikey, int pageservernumber, int rowcount, int? servernumber, int nodeid = 0, string category = null, ExceptionStatus exceptionstatus = ExceptionStatus.NONE, ServerRegion serverregion = ServerRegion.NONE)
        {
            return JsonConvert.DeserializeObject<HorizenNodeAPIDataModels.MyExceptions>(GetQueryJson(apikey.APINodeType, serverregion, servernumber, APIPagedCallUrlParts.MyExceptions(apikey, pageservernumber, rowcount, nodeid, exceptionstatus)));
        }

        /* Path: /api/nodes/my/challenges?key=<apikey>&page=<pageservernumber>&rows=<rowcount>
        * Optional Parameters:   &nid=<nodeid>  return only for specified node.  e.g. &nid=435, &result=<result>  where <result> is 'pass' or 'fail'.   e.g. &result=fail
        * Return: Challenge results for all nodes associated with the API key.
        * Expected return: {"page":1,"total":320,"rowsperpage":"10","records":3199,"rows":[{"id":333694,"fqdn":"zen1.secnodes.com","home":"ts1-testnet.na","nid":9,"start":"2018-06-13T21:53:23.000Z","received":null,"reply":null,"run":null,"result":"wait","reason":null}]}
        */
        public static HorizenNodeAPIDataModels.MyChallenges GetMyChallenges(APIKey apikey, int pageservernumber, int rowcount, int? servernumber, int nodeid = 0, string category = null, ChallengeResult challengestatus = ChallengeResult.NONE, ServerRegion serverregion = ServerRegion.NONE)
        {
            return JsonConvert.DeserializeObject<HorizenNodeAPIDataModels.MyChallenges>(GetQueryJson(apikey.APINodeType, serverregion, servernumber, APIPagedCallUrlParts.MyChallenges(apikey, pageservernumber, rowcount, nodeid, challengestatus)));
        }

        /* Path: /api/nodes/my/payments?key=<apikey>&page=<pageservernumber>&rows=<rowcount>
        * Optional Parameters:    &nid=<nodeid>  return only for specified node.  e.g. &nid=435,  &status=<status>  where <status> is 'exclude'  e.g. &status=exclude
        * Return: Payments and Credits for all nodes associated with the API key. The type is 'e' for earnings and 'c' for credit
        * Expected return: {"page":61,"total":91,"rowsperpage":"10","records":903,"rows":[{"id":243031,"status":"exclude","startdate":"2017-12-13T12:32:32.000Z","enddate":"2017-12-13T18:55:17.000Z","pmid":496,"type":"e","uptime":"0.7774","zen":"0.00000000","created":"2017-12-13T19:24:24.000Z","paidat":null,"txid":null}]}
        */

        public static HorizenNodeAPIDataModels.MyPayments GetMyPayments(APIKey apikey, int pageservernumber, int rowcount, int? servernumber, int nodeid = 0, string category = null, PaymentStatus paymentstatus = PaymentStatus.NONE, ServerRegion serverregion = ServerRegion.NONE)
        {
            return JsonConvert.DeserializeObject<HorizenNodeAPIDataModels.MyPayments>(GetQueryJson(apikey.APINodeType, serverregion, servernumber, APIPagedCallUrlParts.MyPayments(apikey, pageservernumber, rowcount, nodeid, paymentstatus)));
        }
        #endregion


        private static string GetQueryJson(NodeType nodetype, ServerRegion serverregion, int? servernumber, string querystring)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var wc = new WebClient())
            {
                if(serverregion != ServerRegion.NONE && servernumber != null) return wc.DownloadString(APIServer.GetServer(nodetype, serverregion, servernumber) + querystring);
                else if (serverregion != ServerRegion.NONE) return wc.DownloadString(APIServer.GetServer(nodetype, serverregion) + querystring);
                else return wc.DownloadString(APIServer.GetServer(nodetype) + querystring);
            }
        }        
    }
}
