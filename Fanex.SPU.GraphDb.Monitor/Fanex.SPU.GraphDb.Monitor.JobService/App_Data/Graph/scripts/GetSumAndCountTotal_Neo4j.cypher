MATCH (c:CTSCustomer)
WHERE c.CTSCustID < $MaxCTSCustID
WITH COUNT(*) AS CTSCustomerCountTotal, SUM(c.CTSCustID) AS CTSCustomerSumValue
MATCH p=(c:CTSCustomer)-[:USED_DEVICE]->(d:Device)
WHERE c.CTSCustID < $MaxCTSCustID
WITH CTSCustomerCountTotal, CTSCustomerSumValue, COUNT(p) AS AssDevCountTotal, SUM(d.DeviceID) AS AssDevSumDeviceID, SUM(c.CTSCustID) AS AssDevSumCTSCustID
MATCH p=(c:CTSCustomer)-[:ASS_BY_IP]->(t:CTSCustomer)
WHERE c.CTSCustID < $MaxCTSCustID
WITH CTSCustomerCountTotal, CTSCustomerSumValue, AssDevCountTotal, AssDevSumDeviceID, AssDevSumCTSCustID, COUNT(p) AS AssIPCountTotal, SUM(c.CustID) AS AssIPSumFromCustID, SUM(t.CustID) AS AssIPSumToCustID
MATCH p=(c:CTSCustomer)-[:ASS_BY_AI]->(t:CTSCustomer)
WHERE c.CTSCustID < $MaxCTSCustID
WITH CTSCustomerCountTotal, CTSCustomerSumValue, AssDevCountTotal, AssDevSumDeviceID, AssDevSumCTSCustID, AssIPCountTotal, AssIPSumFromCustID, AssIPSumToCustID, COUNT(p) AS AssAICountTotal, SUM(c.CustID) AS AssAISumFromCustID, SUM(t.CustID) AS AssAISumToCustID
MATCH p=(c:CTSCustomer)-[:GROUP_BY_AI]->(g:AIGroup)
WHERE c.CTSCustID < $MaxCTSCustID
WITH CTSCustomerCountTotal, CTSCustomerSumValue, AssDevCountTotal, AssDevSumDeviceID, AssDevSumCTSCustID, AssIPCountTotal, AssIPSumFromCustID, AssIPSumToCustID, AssAICountTotal, AssAISumFromCustID, AssAISumToCustID, COUNT(p) AS AssGroupAICountTotal, SUM(c.CustID) AS AssGroupAISumCustID, SUM(g.GroupID) AS AssGroupAISumGroupID
MATCH p=(c:CTSCustomer)-[:ADD_MANUALLY]->(t:CTSCustomer)
WHERE c.CTSCustID < $MaxCTSCustID
WITH CTSCustomerCountTotal, CTSCustomerSumValue, AssDevCountTotal, AssDevSumDeviceID, AssDevSumCTSCustID, AssIPCountTotal, AssIPSumFromCustID, AssIPSumToCustID, AssAICountTotal, AssAISumFromCustID, AssAISumToCustID, AssGroupAICountTotal, AssGroupAISumCustID, AssGroupAISumGroupID, COUNT(p) AS AssManualCountTotal, SUM(c.CTSCustID) AS AssManualSumFromCTSCustID, SUM(t.CTSCustID) AS AssManualSumToCTSCustID
MATCH p=(c:CTSCustomer)-[:HAS_EVIDENCE]->(e:Evidence)
WHERE c.CTSCustID < $MaxCTSCustID
RETURN CTSCustomerCountTotal, CTSCustomerSumValue, AssDevCountTotal, AssDevSumDeviceID, AssDevSumCTSCustID, AssIPCountTotal, AssIPSumFromCustID, AssIPSumToCustID, AssAICountTotal, AssAISumFromCustID, AssAISumToCustID, AssGroupAICountTotal, AssGroupAISumCustID, AssGroupAISumGroupID, AssManualCountTotal, AssManualSumFromCTSCustID, AssManualSumToCTSCustID, COUNT(p) AS CustEvidenceCountTotal, SUM(c.CTSCustID) AS CustEvidenceSumCTSCustID, SUM(e.EvidenceID) AS CustEvidenceSumEviID