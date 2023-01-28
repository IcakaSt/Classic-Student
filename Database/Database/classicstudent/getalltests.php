<?php
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);
$con = mysqli_connect('localhost','root','root','classic_student');

if(mysqli_connect_errno())
{
    die('{ "errorCode": "1" }');
}

$userId = $_POST["userId"];

//find test
$selecttestquery = "SELECT testId,userId,numberQuestions,title, totalPoints,timer,code FROM tests WHERE userId='" .$userId . "';";
$checkuserid =mysqli_query($con,$selecttestquery) or die('{ "errorCode": "2" }'); // qeury failed

if(mysqli_num_rows($checkuserid) > 0)
{  
    while($row = mysqli_fetch_assoc($checkuserid))
    {
      $test['tests'][] = $row;
    }
    echo json_encode($test);
}
else{die();}
?>