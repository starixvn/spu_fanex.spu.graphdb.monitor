UNWIND $Ass AS ctsAssByIP
OPTIONAL MATCH (fc:CTSCustomer)-[r:ASS_BY_IP]->(tc:CTSCustomer)
WHERE fc.CustID = ctsAssByIP.FromCustID AND fc.CustSubID = 0
    AND tc.CustID = ctsAssByIP.ToCustID AND tc.CustSubID = 0
WITH fc.CustID AS FromCustID, ctsAssByIP.FromCustID AS MissingFromCustID, tc.CustID AS ToCustID, ctsAssByIP.ToCustID AS MissingToCustID, ctsAssByIP.AssByIpID AS AssByIpID
WHERE FromCustID IS NULL
RETURN MissingFromCustID AS FromCustID, MissingToCustID AS ToCustID, AssByIpID