<?php
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);
$con = mysqli_connect('localhost','root','root','classic_student');

if(mysqli_connect_errno())
{
    die('"errorCode": "1"');
}

$email = $_POST["email"];
$password = $_POST["password"];

//check email
$emailcheckquery="SELECT userID, email, password FROM users WHERE email='" .$email . "';";

$userStuff =mysqli_query($con,$emailcheckquery) or die('{ "errorCode": "2" }'); // qeury failed

if(mysqli_num_rows($userStuff) > 0)
{
    $user = mysqli_fetch_assoc($userStuff);
    if (password_verify($password, $user['password'])) {
        echo '{ "userId": "' . $user['userID'] . '", "errorCode": "0" }'; // wrong paassword
    } else {
        echo '{ "errorCode": "3" }'; // wrong paassword
    }
    die();
}


echo '{ "errorCode": "4" }';; //email doesnt exist
?>