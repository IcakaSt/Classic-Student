<?php
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);
$con = mysqli_connect('localhost','root','root','classic_student');

if(mysqli_connect_errno())
{
    die('{ "errorCode": "1" }');
}
$test = json_decode($_POST['test'], true);
do 
{
    $code = mt_rand(1000000, 9999999);

    $codecheckquery="SELECT code FROM tests WHERE code='" .$code . "';";

    $checkcode =mysqli_query($con,$codecheckquery) or die('{ "errorCode": "2" }'); // qeury failed

} while (mysqli_num_rows($checkcode) > 0);

//add test
$insertuserquery = "INSERT INTO tests (userId,numberQuestions,title, totalPoints,timer,code) VALUES ('" . $test['userId'] . "','" . $test['numberQuestions'] . "','" . $test['title'] . "','" . $test['totalPoints'] . "','" . $test['timer'] . "','" . $code . "');";
mysqli_query($con, $insertuserquery) or die ('{ "errorCode": "3" }'); //insert test failed

$testId = $con->insert_id;

foreach($test['questions'] as $question) {
    $insertuserquery = "INSERT INTO questions (testId,answersNumber,points, questionText) VALUES ('" . $testId . "','" . $question['answersNumber'] . "','" . $question['points'] . "','" . $question['questionText'] . "');";
    mysqli_query($con, $insertuserquery) or die ('{ "errorCode": "4" }'); //insert test failed

    $questionId = $con->insert_id;
    foreach($question['answers'] as $answer) {

        $insertuserquery = "INSERT INTO answer (questionId,answerText,correct) VALUES ('" . $questionId . "','" . $answer['answerText'] . "','" . $answer['correct'] .  "');";
        mysqli_query($con, $insertuserquery) or die ('{ "errorCode": "5" }'); //insert test failed
        
    }
}

echo '{ "errorCode": "0" }';
?>