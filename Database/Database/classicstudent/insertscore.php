<?php
$con = mysqli_connect('localhost','root','root','classic_student');

if(mysqli_connect_errno())
{
    die('"errorCode": "1"');
}

$testId = $_POST["testId"];
$points = $_POST["points"];
$studentName = $_POST["studentName"];

//add score
$password = password_hash($password, PASSWORD_BCRYPT);
$insertuserquery = "INSERT INTO scores (studentName, testId,points) VALUES ('" . $studentName . "','" . $testId . "','" . $points . "');";
mysqli_query($con, $insertuserquery) or die ('{ "errorCode": "4" }'); //insert score failed

echo '{ "errorCode": "0" }';
?>