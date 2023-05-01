@ECHO OFF
echo Starting Java application
echo.

echo Setting aws envirmoment variables
echo.

set /p NIMBLEFLOW_AWS_ACCESS_KEY_ID="Type the AWS access key id: "
set /p NIMBLEFLOW_AWS_SECRET_ACCESS_KEY="Type the AWS secret access key: "
set /p NIMBLEFLOW_JASYPT_ENCRIPTION_KEY="Type the Jasypt encryption code: "
echo.

set NIMBLEFLOW_AWS_ACCESS_KEY_ID=%NIMBLEFLOW_AWS_ACCESS_KEY_ID%
set NIMBLEFLOW_AWS_SECRET_ACCESS_KEY=%NIMBLEFLOW_AWS_SECRET_ACCESS_KEY%
set NIMBLEFLOW_JASYPT_ENCRIPTION_KEY=%NIMBLEFLOW_JASYPT_ENCRIPTION_KEY%

echo Here's yout enviroment variables: 
set NIMBLEFLOW_AWS_ACCESS_KEY_ID
set NIMBLEFLOW_AWS_SECRET_ACCESS_KEY
set NIMBLEFLOW_JASYPT_ENCRIPTION_KEY
echo.

echo Strating docker-compose
docker-compose up -d

pause >nul