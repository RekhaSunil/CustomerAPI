using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


public class RetDBAction
{
    private int _mRetValue;
    private bool _mStatus;
    private string _mErDesc;

    public int RetValue
    {
        get { return _mRetValue; }
        set { _mRetValue = value; }
    }

    public bool Status
    {
        get { return _mStatus; }
        set { _mStatus = value; }
    }

    public string ErrorDescription
    {
        get { return _mErDesc; }
        set { _mErDesc = value; }
    }
}

public class RetDataSet
{
    private bool _mStatus;
    private string _mErDesc;

//private string _mConnect;

    public DataSet dataSet;
    public bool Status    {
        get { return _mStatus; }
        set { _mStatus = value; }
    }
    public string ErrorDescription
    {
        get { return _mErDesc; }
        set { _mErDesc = value; }
    }
    //public string ConData
    //{
    //    get { return _mConnect; }
    //    set { _mConnect = value; }
    //}
}
public class RetHuInfo : RetDataSet
{
    public string HuExpected { get; set; }   
}

public class RetDataTable
{
    private bool _mStatus;
    private string _mErDesc;

    //private string _mConnect;

    public DataTable dataTable;
    public bool Status
    {
        get { return _mStatus; }
        set { _mStatus = value; }
    }
    public string ErrorDescription
    {
        get { return _mErDesc; }
        set { _mErDesc = value; }
    }
    //public string ConData
    //{
    //    get { return _mConnect; }
    //    set { _mConnect = value; }
    //}
}


public class RetDevInfo : RetDBAction
{
    public int VersionCode { get; set; }
    public string Title { get; set; }
    public string ReleaseNotes { get; set; }
    public string AppPath { get; set; }
}

public class RetSlocInfo : RetDBAction
{    
    public string materialDocument { get; set; }
    public string materialYear { get; set; }
    public string error { get; set; }
}
public class RetDsSetIn : RetDataSet
{   
    public string TripExpected { get; set; }
    public string TripScanned { get; set; }
    public bool isHemel { get; set; }
}
public class RetDsSetAsn :RetDBAction
{
    public string AsnExpected { get; set; }
    public string AsnScanned { get; set; }
    public string InvoiceNo { get; set; }
    public DataSet dataSet;
}

public class RetAsnWiseList : RetDBAction
{
    public string NoofShortage { get; set; }
    public string NoofAlien { get; set; }
    public string NoofExcess { get; set; }
}
public class HuDetInfoforASN
{
    public string CustID { get; set; }
    public string Tripnumber { get; set; }
    public string AsnNumber { get; set; }
    public string HuNumber { get; set; }
}

public class ExtractEPCValues:RetDBAction
{
    public string UPC { get; set; }
    public string CompanyCode { get; set; }
    public string SerialNo { get; set; }
    public string GTINNumber { get; set; }
    public string RFID { get; set; }
}
public class RetSlocDetails : RetDBAction
{
    public string AsnExpected { get; set; }
    public string AsnScanned { get; set; }
    public string InvoiceNo { get; set; }
    public DataSet dataSet;
}

public class Authentication : RetDBAction
{
    public string UserID { get; set; }
    public string UserName { get; set; }
    public int RoleID { get; set; }
    public int DeptID { get; set; }
    public int UserType { get; set; }
    public int StoreID { get; set; }
    public string StoreName { get; set; }
    public string StoreCode { get; set; }
    public string regToken { get; set; }
    public int isAdmin { get; set; }
    public int isStoreView { get; set; }
}

public class ChangePWD
{
    public string CustID { get; set; }
    public string UserID { get; set; }
    public string OldPasswd { get; set; }
    public string NewPasswd { get; set; }
}

public class ImportData
{
    public DataTable dtDetails { get; set; }
    public string UserID { get; set; }
    public string CustID { get; set; }    
}

public class RoleFieldsJq
{
    public int RoleID { get; set; }
    public string Rolename { get; set; }
    public string CreatedBy { get; set; }
    public int Status { get; set; }
    public int MenuID { get; set; }
    public int allowAdd { get; set; }
    public int allowEdit { get; set; }
    public int allowView { get; set; }
    public int allowdelete { get; set; }
    public int Flag { get; set; }
    public string CustID { get; set; }

    private List<RoleFieldsForgrd> _items;

    public List<RoleFieldsForgrd> Items
    {
        get { return _items; }
        set { _items = value; }
    }
}

public class RoleFieldsForgrd
{
    public int RoleID { get; set; }
    public string Rolename { get; set; }
    public string CreatedBy { get; set; }
    public int Status { get; set; }
    public int MenuID { get; set; }
    public int allowAdd { get; set; }
    public int allowEdit { get; set; }
    public int allowView { get; set; }
    public int allowdelete { get; set; }
    public int Flag { get; set; }
}

public class RFIDDetails:RetDBAction
{
    public string UPC { get; set; }
    public string UserID { get; set; }
    public string Store { get; set; }
    public string ItemName { get; set; }
    public string Department { get; set; }
    public string Stroke { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }
    public string MRP { get; set; }
    public string RFID { get; set; }
    public string GTINNumber { get; set; }
    public string SerialNo { get; set; }
}

public class RetConfirmSave:RetDBAction
{
    public string TripExpected { get; set; }
    public string TripScanned { get; set; }
    public string AsnExpected { get; set; }
    public string AsnScanned { get; set; }
    public string InvoiceNo { get; set; }
    public DataSet dataSet;
    public bool endAsn { get; set; }
    public bool endTrip { get; set; }
    public bool isHemel { get; set; }
}

public class RetResults : RetDataSet
{
    public int TotalPages { get; set; }
    public int total_results { get; set; }
    public int page { get; set; }   
}
public class RFIDBulkDetails : RetDataSet
{
    public string UPC { get; set; }
    public string UserID { get; set; }
    public string Store { get; set; }
    public string ItemName { get; set; }
    public string Department { get; set; }
    public string Stroke { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }
    public string MRP { get; set; }
    public string RFID { get; set; }
    public string GTINNumber { get; set; }
    public string SerialNo { get; set; }
}