UNWIND $Ass AS ctsAssByManual
OPTIONAL MATCH (fc:CTSCustomer)-[r:ADD_MANUALLY]->(tc:CTSCustomer)
WHERE fc.CTSCustID = ctsAssByManual.FromCTSCustID
    AND tc.CTSCustID = ctsAssByManual.ToCTSCustID 
WITH fc.CTSCustID AS FromCTSCustID, ctsAssByManual.FromCTSCustID AS MissingFromID, tc.CTSCustID AS ToCTSCustID, ctsAssByManual.ToCTSCustID AS MissingToID, ctsAssByManual.AssID AS AssID
WHERE FromCTSCustID IS NULL
RETURN MissingFromID AS FromCTSCustID, MissingToID AS ToCTSCustID, AssID