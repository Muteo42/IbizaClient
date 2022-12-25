<?php
	if (!empty($_SERVER['HTTP_CLIENT_IP'])) 
	{
		echo $_SERVER['HTTP_CLIENT_IP'];
		exit;
	}
	else if(!empty($_SERVER['HTTP_X_FORWARDED_FOR'])) 
	{
		echo $_SERVER['HTTP_X_FORWARDED_FOR'];
		exit;
	}
	else
	{
		echo $_SERVER['REMOTE_ADDR'];
		exit;
	}
?>