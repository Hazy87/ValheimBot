resource "aws_dynamodb_table" "stats_table" {
  name = "valheimbot-stats"
  read_capacity = 5
  write_capacity = 5
  hash_key = "UserId"

  attribute {
    name = "UserId"
    type = "S"
  }
}