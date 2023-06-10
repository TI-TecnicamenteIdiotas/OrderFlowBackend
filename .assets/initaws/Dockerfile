FROM amazon/aws-cli

RUN echo '{"Version": "2012-10-17", "Statement":[{"Effect": "Allow", "Principal": "*", "Action": "s3:GetObject", "Resource": "arn:aws:s3:::*"}]}' >> policy.json

ENTRYPOINT aws dynamodb create-table \
           --table-name order \
           --attribute-definitions AttributeName=id,AttributeType=S \
           --key-schema AttributeName=id,KeyType=HASH \
           --provisioned-throughput ReadCapacityUnits=5,WriteCapacityUnits=5 \
           --table-class STANDARD \
           --endpoint-url ${NO_SQL_DATABASE_PATH} \
           --region ${AWS_REGION} \
           && \
           aws s3api create-bucket \
           --bucket ${AWS_S3_BUCKET_NAME} \
           --endpoint-url ${AWS_S3_SERVICE_URL} \
           && \
           aws s3api put-bucket-policy \
           --bucket ${AWS_S3_BUCKET_NAME} \
           --endpoint-url ${AWS_S3_SERVICE_URL} \
           --policy file://policy.json \
           && \
           exit 0