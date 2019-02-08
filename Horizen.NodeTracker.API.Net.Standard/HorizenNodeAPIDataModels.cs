using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Horizen.NodeTracker.API.NET.Standard
{
    public partial class HorizenNodeAPIDataModels
    {
        #region API - Public
        public class ServerStats
        {
            /* API Call: /api/srvstats
             * Expected return: {"server":"ts2.eu","region":"eu","state":"up","nodes":2324,"up":2256,"down":41,"inactive":27,"foreign":1,"regional":{"na":{"total":8853,"up":8564,"down":130,"inactive":159},"eu":{"total":13711,"up":13307,"down":235,"inactive":169}},"global":{"total":22564,"up":21871,"down":365,"inactive":328},"estearn":"0.0329","actualearn":"0.0385","cfCheck":"healthy"}
             */

            public string server { get; set; }
            public string region { get; set; }
            public string state { get; set; }
            public int nodes { get; set; }
            public int up { get; set; }
            public int down { get; set; }
            public int inactive { get; set; }
            public int foreign { get; set; }
            public Regional regional { get; set; }
            public Global global { get; set; }
            public string estearn { get; set; }
            public string actualearn { get; set; }
            public string cfCheck { get; set; }

            public class Regional
            {
                public Na na { get; set; }
                public Eu eu { get; set; }
            }

            public class Na
            {
                public int total { get; set; }
                public int up { get; set; }
                public int down { get; set; }
                public int inactive { get; set; }
            }

            public class Eu
            {
                public int total { get; set; }
                public int up { get; set; }
                public int down { get; set; }
                public int inactive { get; set; }
            }

            public class Global
            {
                public int total { get; set; }
                public int up { get; set; }
                public int down { get; set; }
                public int inactive { get; set; }
            }
        }



        public class ServerList
        {
            /* API Call: /api/srvlist
             * Expected return: {"region":"na","regions":[["na","North America"],["eu","Europe"]],"servers":["ts2.na","ts1.na","ts3.na","ts4.na","ts1.eu","ts2.eu","ts3.eu","ts4.eu"]}
             */
            public string region { get; set; }
            public string[][] regions { get; set; }
            public string[] servers { get; set; }
        }

        public class ServerEarnings
        {
            /* API Call: /api/earnings
             * Expected return: {"lastactual":0.03851091,"stake":42,"zenbtc":0.001212,"btcusd":3651.93113436,"zenusd":4.42614053484432,"zenearned":198375.86162042,"zenpaidusd":"878039.44"}
             */
            public float lastactual { get; set; }
            public int stake { get; set; }
            public float zenbtc { get; set; }
            public float btcusd { get; set; }
            public float zenusd { get; set; }
            public float zenearned { get; set; }
            public string zenpaidusd { get; set; }
        }

        public class ServerOpenChallenges
        {
            /* API Call: /api/chal/open
             * Expected return: {"chalOpenCount":[{"server":"ts1.eu","count":3},{"server":"ts1.na","count":18},{"server":"ts2.eu","count":16},{"server":"ts2.na","count":17},{"server":"ts3.eu","count":1},{"server":"ts3.na","count":9},{"server":"ts4.eu","count":2},{"server":"ts4.na","count":6}]}
             */
            public Chalopencount[] chalOpenCount { get; set; }

            public class Chalopencount
            {
                public string server { get; set; }
                public int count { get; set; }
            }
        }

        public class NodeCertStatus
        {
            /* API Call: /api/node/<node_id>/certstatus
            * Expected return: {"validCert":true,"subject":{"CN":"*.safunodes.win"},"issuer":"Let's Encrypt Authority X3","validTo":"Apr  8 04:04:19 2019 GMT","zenIp":"2a0d:3001:2100:b002:5::63ba","ipMatch":true,"valid":true,"zend":{"zip6":"2a0d:3001:2100:b002:5::63ba","port":"32107"}}            */

            public bool validCert { get; set; }
            public Subject subject { get; set; }
            public string issuer { get; set; }
            public string validTo { get; set; }
            public string zenIp { get; set; }
            public bool ipMatch { get; set; }
            public bool valid { get; set; }
            public Zend zend { get; set; }

            public class Subject
            {
                public string CN { get; set; }
            }

            public class Zend
            {
                public string zip6 { get; set; }
                public string port { get; set; }
            }
        }

        #endregion
        #region Nodes - API  - Non-Paged Requests

        public class NodeDetail
        {
            /* Path: /api/nodes/<nodeid>/detail?key=<apikey> 
             * Return: returns node details for given nodeid.  If the email of the API key matches the email of the node, the node t-address and stake t-address are also returned as 'taddr' and 'stkaddr'. Any open Exceptions and Downtimes are also returned.
            * Expected return: {"id":9,"status":"up","home":"ts1-testnet.na","curserver":"ts1-testnet.na","ip4":"198.58.105.60","ip6":null,"fqdn":"zen1.secnodes.com","config":{"hw":{"CPU":"Intel(R) Xeon(R) CPU E5-2697 v4 @ 2.30GHz","cores":1,"speed":2299},"node":{"version":2001050,"wallet.version":60000,"protocolversion":170002},"trkver":"0.2.1"},"createdAt":"2017-10-16T19:24:11.000Z","updatedAt":"2018-06-13T22:38:25.000Z","taddr":"ztTYagRWzHZuiZxFTuhTgUZigFyoHNBe65s","stkaddr":"ztaj8xNwDjkLxMYWHnWLCvaxHYLmDQzwcxZ","email":"myemail@gmail.com","exceptions":[{"id":40184,"etype":"chalmax","start":"2018-05-02T17:06:40.000Z","check":"2018-06-13T22:40:22.000Z","end":null}],"hasException":true,"downtimes":[],"category":"mynodes"}
            */
            public int id { get; set; }
            public string status { get; set; }
            public string home { get; set; }
            public string curserver { get; set; }
            public object ip4 { get; set; }
            public string ip6 { get; set; }
            public string fqdn { get; set; }
            public Config config { get; set; }
            public DateTime createdAt { get; set; }
            public DateTime updatedAt { get; set; }
            public string taddr { get; set; }
            public string stkaddr { get; set; }
            public string email { get; set; }
            public string category { get; set; }
            public Exception[] exceptions { get; set; }
            public Downtime[] downtimes { get; set; }

            public class Config
            {
                public Hw hw { get; set; }
                public Mem mem { get; set; }
                public float ver { get; set; }
                public Node node { get; set; }
                public string nodejs { get; set; }
                public string trkver { get; set; }
                public string platform { get; set; }
            }

            public class Hw
            {
                public string CPU { get; set; }
                public int cores { get; set; }
                public int speed { get; set; }
            }

            public class Mem
            {
                public string units { get; set; }
                public float memfree { get; set; }
                public float memtotal { get; set; }
                public float swapfree { get; set; }
                public float swaptotal { get; set; }
                public float memavailable { get; set; }
            }

            public class Node
            {
                public int version { get; set; }
                public int walletversion { get; set; }
                public int protocolversion { get; set; }
            }

            public class Downtime
            {
                public int id { get; set; }
                public string dtype { get; set; }
                public DateTime start { get; set; }
                public DateTime check { get; set; }
                public object end { get; set; }
            }

            public class Exception
            {
                public int id { get; set; }
                public string etype { get; set; }
                public DateTime start { get; set; }
                public DateTime check { get; set; }
                public object end { get; set; }
            }
        }

        public class MyNodes
        {
            /* Path: /api/nodes/my/list?key=<apikey>
             * Optional Parameters:  &status=<status>  where <status> can be 'up' or 'down'. e.g. &status=up
             * Return: array of nodes associated with the node email address linked to the API key. 
            * Expected return: {"nodes":[{"id":18323,"status":"up","home":"ts5.eu","curserver":"ts5.eu","ip4":null,"ip6":"2a0d:3001:2100:a003:1::6dc0","fqdn":"cs001.ninjanodes.win","createdAt":"2018-01-12T21:34:30.000Z","updatedAt":"2019-02-08T20:54:47.000Z","email":"nodetracking@gmail.com","category":"cs","zenver":2001650,"trkver":"0.3.1"}]}
            */
            public List<Node> nodes { get; set; }

            public class Node
            {
                public int id { get; set; }
                public string status { get; set; }
                public string home { get; set; }
                public string curserver { get; set; }
                public object ip4 { get; set; }
                public string ip6 { get; set; }
                public string fqdn { get; set; }
                public DateTime createdAt { get; set; }
                public DateTime updatedAt { get; set; }
                public string email { get; set; }
                public string category { get; set; }
                public int zenver { get; set; }
                public string trkver { get; set; }
            }
        }

        public class MyEarnings
        {
            /* Path: /api/nodes/my/earnings?key=<apikey>
             * Optional Parameters:   &nid=<nodeid>  return only for specified node. e.g. &nid=435
             * Return: array of nodes with their associated earnings to date along with a record count and summary data that include the total zen earned to date and the current price of Zen in USD. Data is associated with the node email address linked to the API key. 
            * Expected return: {"records":2,"rows":[{"nid":9,"fqdn":"zen1.secnodes.com","zen":"484.41397152","added":"2017-10-16T19:24:11.000Z"},{"nid":12,"fqdn":"zen102.secnodes.com","zen":"5.22053245","added":"2017-10-16T19:25:06.000Z"}],"summary":{"totalzen":489.63450397,"zenusd":17.18517909073966}}
            */

            public int records { get; set; }
            public Row[] rows { get; set; }
            public Summary summary { get; set; }

            public class Summary
            {
                public float totalzen { get; set; }
                public float zenusd { get; set; }
            }

            public class Row
            {
                public int nid { get; set; }
                public string fqdn { get; set; }
                public string zen { get; set; }
                public DateTime added { get; set; }
            }
        }

        #endregion

        #region Nodes - API  - Paged Requests
        /* The following API calls return paged results.The page number and row count must be passed in the search parameters of the request.  

        Example: api/nodes/my/exceptions? key = 6201c79b86e4ec54048344512f1498c2ed5ba2c0&page= 1 & rows = 10

        The result elements with data about the number of records, total pages and the current page number along with the items requested in the 'rows' array.The number of rows requested is returned as 'rowsperpage'.
        #endregion */


        public class MyDowntimes
        {
            /* Path: /api/nodes/my/downtimes?key=<apikey>&page=<pagenumber>&rows=<rowcount>
             * Optional Parameters:  &nid=<nodeid>  return only for specified node. e.g. &nid=435, &status=<status>  where <status> is 'o' for open or 'c' for closed.  e.g. &status=o
             * Return: Downtimes for all nodes associated with the API key.
            * Expected return: {"page":1,"total":77992,"rowsperpage":1,"records":77992,"rows":[{"id":15522292,"status":"c","fqdn":"pf992.ultrameganodes.win","home":"ts6.eu","curserver":"ts6.eu","start":"2019-02-08T16:10:44.000Z","check":"2019-02-08T16:12:15.000Z","end":"2019-02-08T16:12:40.000Z","duration":116000,"dtype":"zend","nid":165286}]}
            */
            public int page { get; set; }
            public int total { get; set; }
            public int rowsperpage { get; set; }
            public int records { get; set; }
            public Row[] rows { get; set; }

            public class Row
            {
                public int id { get; set; }
                public string status { get; set; }
                public string fqdn { get; set; }
                public string home { get; set; }
                public string curserver { get; set; }
                public DateTime start { get; set; }
                public DateTime check { get; set; }
                public DateTime end { get; set; }
                public int duration { get; set; }
                public string dtype { get; set; }
                public int nid { get; set; }
            }
        }

        public class MyExceptions
        {
            /* Path: /api/nodes/my/exceptions?key=<apikey>&page=<pagenumber>&rows=<rowcount>
             * Optional Parameters:  &nid=<nodeid>  return only for specified node.  e.g. &nid=435,  &status=<status>  where <status> is 'o' for open or 'c' for closed.   e.g. &status=o
             * Return: Exceptions for all nodes associated with the API key.
            * Expected return: {"page":1,"total":89669,"rowsperpage":1,"records":89669,"rows":[{"id":17324776,"status":"c","fqdn":"dgn163.ninjanodes.win","home":"ts3.na","start":"2019-02-08T21:04:11.000Z","check":"2019-02-08T21:05:27.000Z","end":"2019-02-08T21:05:27.000Z","duration":76000,"etype":"cert","nid":123763}]}
            */
            public int page { get; set; }
            public int total { get; set; }
            public int rowsperpage { get; set; }
            public int records { get; set; }
            public Row[] rows { get; set; }

            public class Row
            {
                public int id { get; set; }
                public string status { get; set; }
                public string fqdn { get; set; }
                public string home { get; set; }
                public DateTime start { get; set; }
                public DateTime check { get; set; }
                public DateTime end { get; set; }
                public int duration { get; set; }
                public string etype { get; set; }
                public int nid { get; set; }
            }
        }

        public class MyChallenges
        {
            /* Path: /api/nodes/my/challenges?key=<apikey>&page=<pagenumber>&rows=<rowcount>
             * Optional Parameters:   &nid=<nodeid>  return only for specified node.  e.g. &nid=435, &result=<result>  where <result> is 'pass' or 'fail'.   e.g. &result=fail
             * Return: Challenge results for all nodes associated with the API key.
            * Expected return: {"page":1,"total":320,"rowsperpage":"10","records":3199,"rows":[{"id":333694,"fqdn":"zen1.secnodes.com","home":"ts1-testnet.na","nid":9,"start":"2018-06-13T21:53:23.000Z","received":null,"reply":null,"run":null,"result":"wait","reason":null}]}
            */
            public int page { get; set; }
            public int total { get; set; }
            public int rowsperpage { get; set; }
            public int records { get; set; }
            public Row[] rows { get; set; }

            public class Row
            {
                public int id { get; set; }
                public string fqdn { get; set; }
                public string home { get; set; }
                public int nid { get; set; }
                public DateTime start { get; set; }
                public DateTime received { get; set; }
                public int reply { get; set; }
                public int run { get; set; }
                public string result { get; set; }
                public string reason { get; set; }
            }
        }

        public class MyPayments
        {
            /* Path: /api/nodes/my/payments?key=<apikey>&page=<pagenumber>&rows=<rowcount>
             * Optional Parameters:    &nid=<nodeid>  return only for specified node.  e.g. &nid=435,  &status=<status>  where <status> is 'exclude'  e.g. &status=exclude
             * Return: Payments and Credits for all nodes associated with the API key. The type is 'e' for earnings and 'c' for credit
            * Expected return: {"page":1,"total":338547,"rowsperpage":1,"records":338547,"rows":[{"id":24976690,"status":"exclude","startdate":"2019-02-07T03:11:13.000Z","enddate":"2019-02-08T03:20:19.000Z","pmid":1948,"type":"e","uptime":"0.0000","zen":"0.00000000","created":"2019-02-08T03:35:52.000Z","paidat":null,"txid":null,"creditpmids":null,"nid":96572}]}
            */
            public int page { get; set; }
            public int total { get; set; }
            public int rowsperpage { get; set; }
            public int records { get; set; }
            public Row[] rows { get; set; }

            public class Row
            {
                public int id { get; set; }
                public string status { get; set; }
                public DateTime startdate { get; set; }
                public DateTime enddate { get; set; }
                public int pmid { get; set; }
                public string type { get; set; }
                public string uptime { get; set; }
                public string zen { get; set; }
                public DateTime created { get; set; }
                public DateTime paidat { get; set; }
                public string txid { get; set; }
                public string creditpmids { get; set; }
                public int nid { get; set; }
            }
        }

        #endregion
    }







}
