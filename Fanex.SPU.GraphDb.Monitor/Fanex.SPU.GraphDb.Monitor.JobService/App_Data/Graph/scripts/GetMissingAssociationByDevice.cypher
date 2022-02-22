UNWIND $Ass AS ctsAssByDev
OPTIONAL MATCH (c:CTSCustomer)-[r:USED_DEVICE]->(d:Device)
WHERE c.CTSCustID=ctsAssByDev.CTSCustID AND d.DeviceID=ctsAssByDev.DeviceID
WITH c.CTSCustID AS CTSCustID, ctsAssByDev.CTSCustID AS MissingCTSCustID, ctsAssByDev.DeviceID AS MissingDeviceID, ctsAssByDev.CTSAssDevID AS CTSAssDevID
WHERE CTSCustID IS NULL
RETURN MissingCTSCustID AS CTSCustID, MissingDeviceID AS DeviceID, CTSAssDevID