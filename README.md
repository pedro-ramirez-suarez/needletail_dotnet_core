<h1><a href="https://github.com/pedro-ramirez-suarez/needletailtools"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0;" title="logo" src="https://raw.github.com/pedro-ramirez-suarez/needletailtools/master/logo.png" border="0" alt="logo" width="73" height="56" /></a> Needletail Tools</h1>
<p>The .Net Core version of Needletail tools, for now only the DataAccess library is compatible with .Net Core 1</p>


<h3>DataAccess</h3>
<p>A Micro ORM that is fast and easy to use, this version only supports MSSQL.</p>
<p>This version has the following differences with the full Needletail.DataAccess version</p>
<ul>
<li>Missing attributes used by RAW Framework for scaffolding and validation</li>
<li>Missing View Models</li>
<li>The .Net Core version is only compatible with MSSQL, the full version supports MySQL and SQL CE</li>
<li>For now the library only looks for connection strings in the "appsettings.json" file, future versions will be able to find connection strings in any source defined in a Builder</li>
<li>Performance compared with EF Core is similar for sync operations but is far more faster than EF Core on async operations, The full Needletail version is a lot of faster than EF 6 and previous in sync and async operations.</li>
<ul>

<h2>Using DataAccess</h2>
<a href="https://github.com/pedro-ramirez-suarez/needletailtools/wiki/Using-Needletail#dataaccess">How to use it.</a>
