UNWIND $ctsCustList AS custList
OPTIONAL MATCH (c:CTSCustomer)
WHERE c.CTSCustID = custList
WITH c.CTSCustID AS checkedCusts, custList AS mysqlCusts
WHERE checkedCusts IS NULL
return mysqlCusts AS CTSCustID