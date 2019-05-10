<?php

if (!(isset($_POST['user']) AND isset($_POST['pass']))) {
	#If the variables user and pass are not set return back to the login page
	header("Location: index.html"); 
}

$user = $_POST['user'] ;
$pass = $_POST['pass'] ;

if ($user != 'admin' OR $pass != 'masterpassword') {
	#Wrong credentials = return to the login page, this return http status code 302
	header("Location: index.html"); 
}
echo "Logged in as: <h3>$user</h3>" ;
