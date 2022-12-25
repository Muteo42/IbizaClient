<?php
	if(!isset($_GET['key'])) exit;
	if(!defined("SYSTEM_2")) define("SYSTEM_2", true);
	include('hash.php');
	if(isset($_SERVER["HTTP_CF_CONNECTING_IP"])) $_SERVER['REMOTE_ADDR'] = $_SERVER["HTTP_CF_CONNECTING_IP"];
	require_once("/var/www/south-central.net/public_html/services/iclient/connect.php");
	$sql = $iconn -> prepare('DELETE FROM client WHERE tempname = :tempnick AND ip = :ip');
	$sql -> execute([
		'tempnick' => $_GET['key'],
		'ip' => $_SERVER["REMOTE_ADDR"]
	]);
	exit;
?>