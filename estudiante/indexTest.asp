<html>
    <!--
    <head>
        <title>Campus Virtual USAT</title>
        <link href="Scripts/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <script src="Scripts/js1_12/bootstrap.min.js" type="text/javascript"></script>
        <script src="Scripts/js1_12/jquery-1.12.3.min.js" type="text/javascript"></script>
    </head>
    -->
    <head>

  <meta charset="UTF-8">
  <link rel="shortcut icon" type="image/x-icon" href="https://static.codepen.io/assets/favicon/favicon-8ea04875e70c4b0bb41da869e81236e54394d63638a1ef12fa558a4a835f1164.ico" />
  <link rel="mask-icon" type="" href="https://static.codepen.io/assets/favicon/logo-pin-f2d2b6d2c61838f7e76325261b7195c27224080bc099486ddd6dccb469b8e8e6.svg" color="#111" />
  <title>CodePen - Expanding Column Layout</title>
  <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css">

    <link href="https://fonts.googleapis.com/css?family=Lato" rel="stylesheet">
  
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/normalize/5.0.0/normalize.min.css">

  
      <style>
      * {
  box-sizing: border-box;
}

.strips {
  min-height: 100vh;
  text-align: center;
  overflow: hidden;
  color: white;
}
.strips__strip {
  will-change: width, left, z-index, height;
  position: absolute;
  width: 20%;
  min-height: 100vh;
  overflow: hidden;
  cursor: pointer;
  transition: all 0.6s cubic-bezier(0.23, 1, 0.32, 1);
}
.strips__strip:nth-child(1) {
  left: 0;
}
.strips__strip:nth-child(2) {
  left: 20vw;
}
.strips__strip:nth-child(3) {
  left: 40vw;
}
.strips__strip:nth-child(4) {
  left: 60vw;
}
.strips__strip:nth-child(5) {
  left: 80vw;
}
.strips__strip:nth-child(1) .strip__content {
  background: #244F75;
  -webkit-transform: translate3d(-100%, 0, 0);
          transform: translate3d(-100%, 0, 0);
  -webkit-animation-name: strip1;
          animation-name: strip1;
  -webkit-animation-delay: 0.1s;
          animation-delay: 0.1s;
}
.strips__strip:nth-child(2) .strip__content {
  background: #60BFBF;
  -webkit-transform: translate3d(0, 100%, 0);
          transform: translate3d(0, 100%, 0);
  -webkit-animation-name: strip2;
          animation-name: strip2;
  -webkit-animation-delay: 0.2s;
          animation-delay: 0.2s;
}
.strips__strip:nth-child(3) .strip__content {
  background: #8C4B7E;
  -webkit-transform: translate3d(0, -100%, 0);
          transform: translate3d(0, -100%, 0);
  -webkit-animation-name: strip3;
          animation-name: strip3;
  -webkit-animation-delay: 0.3s;
          animation-delay: 0.3s;
}
.strips__strip:nth-child(4) .strip__content {
  background: #F8BB44;
  -webkit-transform: translate3d(0, 100%, 0);
          transform: translate3d(0, 100%, 0);
  -webkit-animation-name: strip4;
          animation-name: strip4;
  -webkit-animation-delay: 0.4s;
          animation-delay: 0.4s;
}
.strips__strip:nth-child(5) .strip__content {
  background: #F24B4B;
  -webkit-transform: translate3d(100%, 0, 0);
          transform: translate3d(100%, 0, 0);
  -webkit-animation-name: strip5;
          animation-name: strip5;
  -webkit-animation-delay: 0.5s;
          animation-delay: 0.5s;
}
@media screen and (max-width: 760px) {
  .strips__strip {
    min-height: 20vh;
  }
  .strips__strip:nth-child(1) {
    top: 0;
    left: 0;
    width: 100%;
  }
  .strips__strip:nth-child(2) {
    top: 20vh;
    left: 0;
    width: 100%;
  }
  .strips__strip:nth-child(3) {
    top: 40vh;
    left: 0;
    width: 100%;
  }
  .strips__strip:nth-child(4) {
    top: 60vh;
    left: 0;
    width: 100%;
  }
  .strips__strip:nth-child(5) {
    top: 80vh;
    left: 0;
    width: 100%;
  }
}
.strips .strip__content {
  -webkit-animation-duration: 1s;
          animation-duration: 1s;
  -webkit-animation-timing-function: cubic-bezier(0.23, 1, 0.32, 1);
          animation-timing-function: cubic-bezier(0.23, 1, 0.32, 1);
  -webkit-animation-fill-mode: both;
          animation-fill-mode: both;
  display: flex;
  align-items: center;
  justify-content: center;
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  text-decoration: none;
}
.strips .strip__content:hover:before {
  -webkit-transform: skew(-30deg) scale(3) translate(0, 0);
          transform: skew(-30deg) scale(3) translate(0, 0);
  opacity: 0.1;
}
.strips .strip__content:before {
  content: "";
  position: absolute;
  z-index: 1;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: white;
  opacity: 0.05;
  -webkit-transform-origin: center center;
          transform-origin: center center;
  -webkit-transform: skew(-30deg) scaleY(1) translate(0, 0);
          transform: skew(-30deg) scaleY(1) translate(0, 0);
  transition: all 0.6s cubic-bezier(0.23, 1, 0.32, 1);
}
.strips .strip__inner-text {
  will-change: transform, opacity;
  position: absolute;
  z-index: 5;
  top: 50%;
  left: 50%;
  width: 70%;
  -webkit-transform: translate(-50%, -50%) scale(0.5);
          transform: translate(-50%, -50%) scale(0.5);
  opacity: 0;
  transition: all 0.6s cubic-bezier(0.23, 1, 0.32, 1);
}
.strips__strip--expanded {
  width: 100%;
  top: 0 !important;
  left: 0 !important;
  z-index: 3;
  cursor: default;
}
@media screen and (max-width: 760px) {
  .strips__strip--expanded {
    min-height: 100vh;
  }
}
.strips__strip--expanded .strip__content:hover:before {
  -webkit-transform: skew(-30deg) scale(1) translate(0, 0);
          transform: skew(-30deg) scale(1) translate(0, 0);
  opacity: 0.05;
}
.strips__strip--expanded .strip__title {
  opacity: 0;
}
.strips__strip--expanded .strip__inner-text {
  opacity: 1;
  -webkit-transform: translate(-50%, -50%) scale(1);
          transform: translate(-50%, -50%) scale(1);
}

.strip__title {
  display: block;
  margin: 0;
  position: relative;
  z-index: 2;
  width: 100%;
  font-size: 3.5vw;
  color: white;
  transition: all 0.6s cubic-bezier(0.23, 1, 0.32, 1);
}
@media screen and (max-width: 760px) {
  .strip__title {
    font-size: 28px;
  }
}

.strip__close {
  position: absolute;
  right: 3vw;
  top: 3vw;
  opacity: 0;
  z-index: 10;
  transition: all 0.6s cubic-bezier(0.23, 1, 0.32, 1);
  cursor: pointer;
  transition-delay: 0.5s;
}
.strip__close--show {
  opacity: 1;
}

@-webkit-keyframes strip1 {
  0% {
    -webkit-transform: translate3d(-100%, 0, 0);
            transform: translate3d(-100%, 0, 0);
  }
  100% {
    -webkit-transform: translate3d(0, 0, 0);
            transform: translate3d(0, 0, 0);
  }
}

@keyframes strip1 {
  0% {
    -webkit-transform: translate3d(-100%, 0, 0);
            transform: translate3d(-100%, 0, 0);
  }
  100% {
    -webkit-transform: translate3d(0, 0, 0);
            transform: translate3d(0, 0, 0);
  }
}
@-webkit-keyframes strip2 {
  0% {
    -webkit-transform: translate3d(0, 100%, 0);
            transform: translate3d(0, 100%, 0);
  }
  100% {
    -webkit-transform: translate3d(0, 0, 0);
            transform: translate3d(0, 0, 0);
  }
}
@keyframes strip2 {
  0% {
    -webkit-transform: translate3d(0, 100%, 0);
            transform: translate3d(0, 100%, 0);
  }
  100% {
    -webkit-transform: translate3d(0, 0, 0);
            transform: translate3d(0, 0, 0);
  }
}
@-webkit-keyframes strip3 {
  0% {
    -webkit-transform: translate3d(0, -100%, 0);
            transform: translate3d(0, -100%, 0);
  }
  100% {
    -webkit-transform: translate3d(0, 0, 0);
            transform: translate3d(0, 0, 0);
  }
}
@keyframes strip3 {
  0% {
    -webkit-transform: translate3d(0, -100%, 0);
            transform: translate3d(0, -100%, 0);
  }
  100% {
    -webkit-transform: translate3d(0, 0, 0);
            transform: translate3d(0, 0, 0);
  }
}
@-webkit-keyframes strip4 {
  0% {
    -webkit-transform: translate3d(0, 100%, 0);
            transform: translate3d(0, 100%, 0);
  }
  100% {
    -webkit-transform: translate3d(0, 0, 0);
            transform: translate3d(0, 0, 0);
  }
}
@keyframes strip4 {
  0% {
    -webkit-transform: translate3d(0, 100%, 0);
            transform: translate3d(0, 100%, 0);
  }
  100% {
    -webkit-transform: translate3d(0, 0, 0);
            transform: translate3d(0, 0, 0);
  }
}
@-webkit-keyframes strip5 {
  0% {
    -webkit-transform: translate3d(100%, 0, 0);
            transform: translate3d(100%, 0, 0);
  }
  100% {
    -webkit-transform: translate3d(0, 0, 0);
            transform: translate3d(0, 0, 0);
  }
}
@keyframes strip5 {
  0% {
    -webkit-transform: translate3d(100%, 0, 0);
            transform: translate3d(100%, 0, 0);
  }
  100% {
    -webkit-transform: translate3d(0, 0, 0);
            transform: translate3d(0, 0, 0);
  }
}
/* Demo purposes */
body {
  font-family: 'Lato';
  -webkit-font-smoothing: antialiased;
  text-rendering: geometricPrecision;
  line-height: 1.5;
}

h1, h2 {
  font-weight: 300;
}

.fa {
  font-size: 30px;
  color: white;
}

h2 {
  font-size: 36px;
  margin: 0 0 16px;
}

p {
  margin: 0 0 16px;
}

    </style>

  <script>
      window.console = window.console || function(t) { };
</script>

  
  
  <script>
      if (document.location.search.match(/type=embed/gi)) {
          window.parent.postMessage("resize", "*");
      }
</script>


</head>

<body translate="no" >

  <section class="strips">
  <article class="strips__strip">
    <div class="strip__content">
      <h1 class="strip__title" data-name="Lorem">Awesome</h1>
      <div class="strip__inner-text">
        <h2>Ettrics</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Officia sapiente deserunt consectetur, quod reiciendis corrupti quo ea aliquid! Repellendus numquam quo, voluptate. Suscipit soluta omnis quibusdam facilis, illo voluptates odit!</p>
        <p>
          <a href="https://twitter.com/ettrics" target="_blank"><i class="fa fa-twitter"></i></a>
        </p>
      </div>
      
    </div>
  </article>
  <article class="strips__strip">
    <div class="strip__content">
      <h1 class="strip__title" data-name="Ipsum">Words</h1>
      <div class="strip__inner-text">
        <h2>Ettrics</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Officia sapiente deserunt consectetur, quod reiciendis corrupti quo ea aliquid! Repellendus numquam quo, voluptate. Suscipit soluta omnis quibusdam facilis, illo voluptates odit!</p>
        <p>
          <a href="https://twitter.com/ettrics" target="_blank"><i class="fa fa-twitter"></i></a>
        </p>
      </div>
    </div>
  </article>
  <article class="strips__strip">
    <div class="strip__content">
      <h1 class="strip__title" data-name="Dolor">Go</h1>
      <div class="strip__inner-text">
        <h2>Ettrics</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Officia sapiente deserunt consectetur, quod reiciendis corrupti quo ea aliquid! Repellendus numquam quo, voluptate. Suscipit soluta omnis quibusdam facilis, illo voluptates odit!</p>
        <p>
          <a href="https://twitter.com/ettrics" target="_blank"><i class="fa fa-twitter"></i></a>
        </p>
      </div>
    </div>
  </article>
  <article class="strips__strip">
    <div class="strip__content">
      <h1 class="strip__title" data-name="Sit">Inside</h1>
      <div class="strip__inner-text">
        <h2>Ettrics</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Officia sapiente deserunt consectetur, quod reiciendis corrupti quo ea aliquid! Repellendus numquam quo, voluptate. Suscipit soluta omnis quibusdam facilis, illo voluptates odit!</p>
        <p>
          <a href="https://twitter.com/ettrics" target="_blank"><i class="fa fa-twitter"></i></a>
        </p>
      </div>
    </div>
  </article>
  <article class="strips__strip">
    <div class="strip__content">
      <h1 class="strip__title" data-name="Amet">Here</h1>
      <div class="strip__inner-text">
        <h2>Ettrics</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Officia sapiente deserunt consectetur, quod reiciendis corrupti quo ea aliquid! Repellendus numquam quo, voluptate. Suscipit soluta omnis quibusdam facilis, illo voluptates odit!</p>
        <p>
          <a href="https://twitter.com/ettrics" target="_blank"><i class="fa fa-twitter"></i></a>
        </p>
      </div>
    </div>
  </article>
  <i class="fa fa-close strip__close"></i>
</section>
    <script src="//static.codepen.io/assets/common/stopExecutionOnTimeout-41c52890748cd7143004e05d3c5f786c66b19939c4500ce446314d1748483e13.js"></script>

  <script src='//cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>

  

    <script >
        var Expand = (function() {
            var tile = $('.strips__strip');
            var tileLink = $('.strips__strip > .strip__content');
            var tileText = tileLink.find('.strip__inner-text');
            var stripClose = $('.strip__close');

            var expanded = false;

            var open = function() {

                var tile = $(this).parent();

                if (!expanded) {
                    tile.addClass('strips__strip--expanded');
                    // add delay to inner text
                    tileText.css('transition', 'all .5s .3s cubic-bezier(0.23, 1, 0.32, 1)');
                    stripClose.addClass('strip__close--show');
                    stripClose.css('transition', 'all .6s 1s cubic-bezier(0.23, 1, 0.32, 1)');
                    expanded = true;
                }
            };

            var close = function() {
                if (expanded) {
                    tile.removeClass('strips__strip--expanded');
                    // remove delay from inner text
                    tileText.css('transition', 'all 0.15s 0 cubic-bezier(0.23, 1, 0.32, 1)');
                    stripClose.removeClass('strip__close--show');
                    stripClose.css('transition', 'all 0.2s 0s cubic-bezier(0.23, 1, 0.32, 1)')
                    expanded = false;
                }
            }

            var bindActions = function() {
                tileLink.on('click', open);
                stripClose.on('click', close);
            };

            var init = function() {
                bindActions();
            };

            return {
                init: init
            };

        } ());

        Expand.init();
        //# sourceURL=pen.js
    </script>



  
  

  <script src="https://static.codepen.io/assets/editor/live/css_reload-2a5c7ad0fe826f66e054c6020c99c1e1c63210256b6ba07eb41d7a4cb0d0adab.js"></script>
</body>
</html>