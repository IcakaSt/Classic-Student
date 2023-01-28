<?php
$con = mysqli_connect('localhost','root','root','classic_student');

if(mysqli_connect_errno())
{
    die('"errorCode": "1"');
}

$testId = $_POST["testId"];

//check email
$testidcheckquery="SELECT scoreId,testId,studentName,points FROM scores WHERE testId='" .$testId . "';";

$testidcheck=mysqli_query($con,$testidcheckquery) or die('{ "errorCode": "2" }'); //error testid check failed

if(mysqli_num_rows($testidcheck) > 0)
{
    while($row = mysqli_fetch_assoc($testidcheck))
    {
      $score['scores'][] = $row;
    }
    echo json_encode($score);
}

?>