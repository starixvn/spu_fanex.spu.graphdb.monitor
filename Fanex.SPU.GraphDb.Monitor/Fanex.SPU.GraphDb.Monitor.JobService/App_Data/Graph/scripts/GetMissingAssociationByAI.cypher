UNWIND $Ass AS ctsAssByAI
OPTIONAL MATCH (fc:CTSCustomer)-[r:ASS_BY_AI]->(tc:CTSCustomer)
WHERE fc.CustID = ctsAssByAI.FromCustID AND fc.CustSubID = 0
    AND tc.CustID = ctsAssByAI.ToCustID AND tc.CustSubID = 0
WITH fc.CustID AS FromCustID, ctsAssByAI.FromCustID AS MissingFromCustID, tc.CustID AS ToCustID, ctsAssByAI.ToCustID AS MissingToCustID, ctsAssByAI.AssID AS AssID
WHERE FromCustID IS NULL
RETURN MissingFromCustID AS FromCustID, MissingToCustID AS ToCustID, AssID