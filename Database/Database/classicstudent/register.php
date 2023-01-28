<?php
$con = mysqli_connect('localhost','root','root','classic_student');

if(mysqli_connect_errno())
{
    die('"errorCode": "1"');
}

$email = $_POST["email"];
$password = $_POST["password"];

//check email
$emailcheckquery="SELECT email FROM users WHERE email='" .$email . "';";

$emailcheck=mysqli_query($con,$emailcheckquery) or die('{ "errorCode": "2" }'); //error email check failed

if(mysqli_num_rows($emailcheck) > 0)
{
    echo '{ "errorCode": "3" }'; //email exists
    die();
}

//add user
$password = password_hash($password, PASSWORD_BCRYPT);
$insertuserquery = "INSERT INTO users (email, password) VALUES ('" . $email . "','" . $password . "');";
mysqli_query($con, $insertuserquery) or die ('{ "errorCode": "4" }'); //insert player failed

echo '{ "errorCode": "0" }';
?>