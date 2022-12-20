variable "local_dynamo" {
  description = "Endpoint for local dynamodb"
  default = "http://192.168.10.4:8000"
}


provider "aws" {
  region = "us-east-1"
  skip_credentials_validation = true
  skip_requesting_account_id  = true
  skip_metadata_api_check     = true
  s3_force_path_style         = true
  access_key                  = "mock_access_key"
  secret_key                  = "mock_secret_key"
  endpoints {
    dynamodb = "${var.local_dynamo}"
  }
}