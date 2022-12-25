<?php
	if(!isset($_GET['key'])) exit;
	if(!defined("SYSTEM_2")) define("SYSTEM_2", true);
	include('hash.php');
	$t_securitykey = SifreCoz($_GET['key']);
	$t_response = "";
	switch($t_securitykey)
	{
		case "BAGUVIX":
		{
			$t_response = "Hack|Cheat|Trainer|Inject|HxD|dnSpy|ILSpy|Track|Aimbot";
			break;
		}
		case "HESOYAM":
		{
			$t_response = "14861736|14867368|117760";
			break;
		}
	}
	echo Sifrele($t_response);
?>