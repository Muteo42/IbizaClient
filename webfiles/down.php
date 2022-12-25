<?php
	if(!isset($_GET['key'])) exit;
	if(!defined("SYSTEM_2")) define("SYSTEM_2", true);
	include('hash.php');
	$t_Array = explode("|", SifreCoz($_GET['key']));
	if(count($t_Array) != 3)
	{
		echo Sifrele("NULL");
		exit;
	}
	if(strlen($t_Array[0]) < 7 || strlen($t_Array[1]) < 10)
	{
		echo Sifrele("NULL");
		exit;
	}
	require_once("/var/www/south-central.net/public_html/services/iclient/connect.php");
	if (!empty($_SERVER['HTTP_CLIENT_IP'])) $_SERVER['HTTP_CF_CONNECTING_IP']=$_SERVER['HTTP_CLIENT_IP'];
	elseif (!empty($_SERVER['HTTP_X_FORWARDED_FOR'])) $_SERVER['HTTP_CF_CONNECTING_IP']=$_SERVER['HTTP_X_FORWARDED_FOR'];
	else $_SERVER['HTTP_CF_CONNECTING_IP']=$_SERVER['REMOTE_ADDR'];
	$sql = $iconn -> prepare('SELECT verified FROM client WHERE tempname = :temp AND MSERIAL = :serial AND ip = :ip');
	$sql -> execute([
		'temp' => $t_Array[0],
		'serial' => $t_Array[1],
		'ip' => $_SERVER['HTTP_CF_CONNECTING_IP']
	]);
	if($sql && $sql -> rowCount() > 0)
	{
		$row = $sql -> fetch(PDO::FETCH_ASSOC);
		$mint = (int)$row['verified'];
		if($t_Array[2] - 2 > $mint || $t_Array[2] + 2 < $mint)
		{
			die("-1");
		}
		$sql = $iconn -> prepare('UPDATE client SET verified = :tint WHERE tempname = :temp AND MSERIAL = :serial AND ip = :ip');
		$sql -> execute([
			'temp' => $t_Array[0],
			'serial' => $t_Array[1],
			'tint' => (int)$t_Array[2],
			'ip' => $_SERVER['HTTP_CF_CONNECTING_IP']
		]);
		if($sql) die("1");
	}
	else die("-1");
?>