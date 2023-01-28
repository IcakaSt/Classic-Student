<?php
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);
$con = mysqli_connect('localhost','root','root','classic_student');

if(mysqli_connect_errno())
{
    die('{ "errorCode": "1" }');
}

$code = $_POST["code"];

//find test
$selecttestquery = "SELECT testId,userId,numberQuestions,title, totalPoints,timer,code FROM tests WHERE code='" .$code . "';";
$checkcode =mysqli_query($con,$selecttestquery) or die('{ "errorCode": "2" }'); // qeury failed

if(mysqli_num_rows($checkcode) > 0)
{  
    $test=mysqli_fetch_assoc($checkcode);

    $selectquestionsquery = "SELECT questionId,testId,answersNumber,points, questionText FROM questions WHERE testId='" .$test['testId'] . "';";
    $checkquestions =mysqli_query($con,$selectquestionsquery) or die('{ "errorCode": "3" }'); // qeury failed

    while($row = mysqli_fetch_assoc($checkquestions))
    {
      $test['questions'][] = $row;
    }

    for ($x = 0; $x < $test['numberQuestions']; $x++)
    {
      $selectanswerssquery = "SELECT answerId,questionId,answerText,correct FROM answer WHERE questionId='" .$test['questions'][$x]['questionId'] . "';";
      $checkanswers =mysqli_query($con,$selectanswerssquery) or die('{ "errorCode": "4" }'); // qeury failed
  
      while($row = mysqli_fetch_assoc($checkanswers))
      {
        $test['questions'][$x]['answers'][] = $row;
      }
    }

  echo json_encode($test);
}
else{die();}
?>