UNWIND $CustClassification AS custClassification
OPTIONAL MATCH (c:CTSCustomer)-[r:HAS_CATEGORY]->(ct:CustomerCategory)
WHERE c.CustID=custClassification.CustID 
	AND c.CustSubID = 0 
	AND ct.CategoryID = custClassification.CategoryID
WITH c.CustID AS CustID, custClassification.CustID AS MissingCustID, custClassification.CategoryID AS MissingCategoryID, custClassification.CategoryID AS CategoryID
WHERE CustID IS NULL
RETURN MissingCustID AS CustID, MissingCategoryID AS CategoryID