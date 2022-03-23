UNWIND $CustEvidence AS custEvidence
OPTIONAL MATCH (c:CTSCustomer)-[:HAS_EVIDENCE]->(e:Evidence)
WHERE c.CTSCustID = custEvidence.CTSCustID 
	AND e.EvidenceID = custEvidence.EvidenceID
WITH c.CTSCustID AS CTSCustID
	, custEvidence.CTSCustID AS MissingCTSCustID
	, custEvidence.EvidenceID AS MissingEvidenceID
	, custEvidence.CustEvidID AS MissingCustEvidID
WHERE CTSCustID IS NULL
RETURN MissingCustEvidID AS CustEvidID
	, MissingCTSCustID AS CTSCustID
	, MissingEvidenceID AS EvidenceID