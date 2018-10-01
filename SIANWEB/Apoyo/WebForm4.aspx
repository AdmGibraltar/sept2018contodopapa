<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="SIANWEB.Apoyo.WebForm4" %>


<html lang="es" >

  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Encuesta</title>

    <!-- Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <!-- Custom CSS -->
    <link href="css/main.css" rel="stylesheet">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
	<script src="js/jquery.nav.js"></script>
	<script src="js/functions.js"></script>
  </head>
  <body>

    <!-- NAV BAR -->
    <nav class="navbar navbar-inverse" role="navigation">
      <div class="navbar-inner">
        <div class="container">
          <p class="navbar-text navbar-left nombre-plugin">Encuesta en una sola pagina</p>
        </div>
      </div>
    </nav>

	<!-- JQUERY NAV BAR -->
<div id="container-nav-div" class="container col-xs-12 container-nav">
    <nav class="navbar navbar-default menu-onepage" role="navigation">
	<div class="container-fluid">
	<!-- Brand and toggle get grouped for better mobile display -->
	    <div class="navbar-header">
		    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
		    <span class="sr-only">Toggle navigation</span>
		    <span class="icon-bar"></span>
		    <span class="icon-bar"></span>
		    <span class="icon-bar"></span>
		    </button>  
	    </div>

	<!-- Collect the nav links, forms, and other content for toggling -->
	    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
		    <ul id="onepage-nav" class="nav navbar-nav">
		    <li><a href="#onepage-intro">Intro</a></li>
		    <li><a href="#onepage-Q1">Q1</a></li>
		    <li><a href="#onepage-Q2">Q2</a></li>
		    <li><a href="#onepage-Q3">Q3</a></li>
		    <li><a href="#onepage-Q4">Q4</a></li>
		    </ul>
	    </div><!-- /.navbar-collapse -->
	</div><!-- /.container-fluid -->
    </nav>
    </div>
    <!-- MAIN CONTENT -->
    <div class="container col-xs-12">
    <!-- Intro -->
      <div class="row">
        <div id="onepage-intro" class="section">
			<h2>Encuesta de Satisfaccion</h2>
            <p>Key quieres saber qué opinan sus clientes sobre la empresa. <br />
                Las encuestas de satisfacción del cliente pueden ayudarte a descubrir qué piensan las personas acerca de tu empresa, obtener comentarios sobre el servicio al cliente y mucho más.</p>
        </div>
      </div>
      
      <!-- Pregunta 1 -->
      <div class="row">
        <div id="onepage-Q1" class="section">
		    <h2>Pregunta 1</h2>
		    <p>Qué tan importante es el conocimiento de la industria a la hora de elegir entre diversas empresas como la nuestra?</p><br />
            <p>
                <div data-sm-radio-button="" class="radio-button-container ">
                    <input id="194070468_2413565679" name="194070468" type="radio" class="radio-button-input " value="2413565679">
                    <label data-sm-radio-button-label="" class="answer-label radio-button-label no-touch touch-sensitive clearfix" for="194070468_2413565679">
                    <span class="radio-button-display "></span>
                    <span class="radio-button-label-text question-body-font-theme user-generated ">Extremadamente importante</span>
                    </label>
                </div>
                <div data-sm-radio-button="" class="radio-button-container ">   
                    <input id="194070468_2413565681" name="194070468" type="radio" class="radio-button-input " value="2413565681">
                    <label data-sm-radio-button-label="" class="answer-label radio-button-label no-touch touch-sensitive clearfix" for="194070468_2413565681">
                        <span class="radio-button-display "></span>
                        <span class="radio-button-label-text question-body-font-theme user-generated ">Muy importante</span>
                    </label>
                </div>
                
                <input type="radio" id="tres" name="P1" />Un poco importante<br/>
                <input type="radio" id="cuatro" name="P1" />Ligeramente importante<br/>
                <input type="radio" id="Radio1" name="P1" />Nada importante<br/>
                
            </p>
        </div>
      </div>

      <!-- Pregunta 2 -->
      <div class="row">
        <div id="onepage-Q2" class="section">
			<h2>Pregunta 2</h2>
			<p>¿Qué tan importante es la antigüedad comercial a la hora de elegir entre diversas empresas como la nuestra?</p><br />
            <p><a href="#onepage-Q3">
                <input type="radio" id="Radio2" name="P2" />Extremadamente importante<br/>
                <input type="radio" id="Radio3" name="P2" />Muy importante<br/>
                <input type="radio" id="Radio4" name="P2" />Un poco importante<br/>
                <input type="radio" id="Radio5" name="P2" />Ligeramente importante<br/>
                <input type="radio" id="Radio6" name="P2" />Nada importante<br/>
                </a>
            </p>
        </div>
      </div>

      <!-- Pregunta 3 -->
      <div class="row">
        <div id="onepage-Q3" class="section">
		    <h2>Pregunta 3</h2>
		    <p>¿Qué tan importante es el costo a la hora de elegir entre diversas empresas como la nuestra?</p><br />
            <p><a href="#onepage-Q4">
                <input type="radio" id="Radio7" name="P3" />Extremadamente importante<br/>
                <input type="radio" id="Radio8" name="P3" />Muy importante<br/>
                <input type="radio" id="Radio9" name="P3" />Un poco importante<br/>
                <input type="radio" id="Radio10" name="P3" />Ligeramente importante<br/>
                <input type="radio" id="Radio11" name="P3" />Nada importante<br/>
                </a>
            </p>
        </div>
      </div>

      <!-- Pregunta 4 -->
      <div class="row">
        <div id="onepage-Q4" class="section">
			<h2>Pregunta 4</h2>
			<p>Califique la calidad general de nuestros productos y servicios.</p><br />
            <p><a href="#onepage-intro">
                <input type="radio" id="Radio12" name="P4" />Excelente<br/>
                <input type="radio" id="Radio13" name="P4" />Muy buena<br/>
                <input type="radio" id="Radio14" name="P4" />Buena<br/>
                <input type="radio" id="Radio15" name="P4" />Regular<br/>
                <input type="radio" id="Radio16" name="P4" />Pobre<br/>
                </a>
            </p>
        </div>
      </div>
    </div>
	
</body>
</html>