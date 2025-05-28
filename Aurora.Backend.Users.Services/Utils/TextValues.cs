namespace Aurora.Backend.Users.Services.Utils
{
	public class TextValues
	{
		public const string MODEL_IS_NOT_VALID = "The model isn't valid";
		public const string AUTH_NOT_VALID = "Unauthorized";
        public const string DB_NOT_RECOVERY = "Database connection not recovery";
        public const string RANGE_DATES_NOT_VALID = "The start date cannot be grater than the end date";
        public const string FORMAT_DATE_IS_NOT_VALID = "The format date is not valid, try this: yyyy/MM/dd HH:mm:ss";
		public const string SUBCUSTOMER_ID_NOT_VALID = "Sub Customer not valid";
		public const string PERSONID_IS_NOT_VALID = "Identity not valid";
		public const string OPERATION_SUCCESS = "Operation success";
		public const string GENERAL_ERROR = "Internal Error";
		public const string NUMBER_PHONE_IS_NOT_VALID = "Number phone is not valid, try this: 00|000000000";
		public const string TRANSACTION_NOT_FOUND_BY_PERSON = "Transaction not found for this identity";
		public const string INVITATION_ID_NOT_VALID = "Invitation not valid";
		public const string OBJECT_NOT_EXISTS = "There is no element for this id";
		public const string OBJECT_EXISTS = "An element with the same parameters already exists";
		public const string ROLE_ID_NOT_VALID = "Role is not valid";
		public const string CUSTOMER_ID_IS_NOT_VALID = "Customer is not valid";
		public const string SUBPROJECT_ID_NOT_VALID = "SubCustomer is not valid";
		public const string EMAIL_NOT_VALID = "Email format is not valid";
		public const string NOT_FOUND = "Data not found";
		public const string SUBCUST_CUST_NOT_VALID = "Customer and SubCustomer combination not valid";
		public const string CREDENTIALS_NOT_VALID = "Credentials not valid.";
		public const string ESTATE_IS_NOT_VALID = "State is not valid.";
		public const string PERSON_IS_NOT_ACTIVE = "Identity not active.";
		public const string PLACE_ID_IS_NOT_VALID = "Place is not valid.";
		public const string LOCATION_NOT_VALID = "Location is not valid.";
		
		// Notifications
		public const string LIVENESS_FAILED = "Liveness failed";
		public const string WEBHOOK_NO_DATA = "No data for validate on webhook";
		public const string NOT_EMAIL_TEMPLATE = "Email template not provided";
		public const string NOT_EMAIL = "Email not provided";
		public const string NOT_SMS_TEMPLATE = "Sms template not provided";
		public const string NOT_SMS = "Sms not provided";
    }
}

