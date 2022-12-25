<?php
	require_once("/var/www/south-central.net/public_html/services/iclient/connect.php");
	$sql = $iconn -> prepare('SELECT sqlid FROM oyuncular');
	$sql -> execute();
	if($sql && $sql -> rowCount() > 0)
	{
		echo $sql -> rowCount();
	}
?>