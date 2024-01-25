

    window.addEventListener("scroll",function(){
        // console.log(this.pageYOffset)
        if(this.pageYOffset > 60){
            
            document.querySelector(".header").classList.add("sticky");
        }
        else {
            document.querySelector(".header").classList.remove("sticky")
        }
    })
     
     const side = document.querySelector(".inside") ;
      side.addEventListener("click",function(e){
          if(e.target.classList.contains("box-size")&& !e.target.classList.contains("active"))
          {
              const target = e.target.getAttribute("data-target");
              // console.log(target);
              side.querySelector(".active").classList.remove("active");
              e.target.classList.add("active");
              const center = document.querySelector(".facitity");
              center.querySelector(".box-big.active").classList.remove("active");
              center.querySelector(target).classList.add("active");
      
           }
      })
      //-----------------Animation-----------
      AOS.init()
