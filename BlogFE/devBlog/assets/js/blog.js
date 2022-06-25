$(document).ready(function() {
	
	/* ======= Highlight.js Plugin ======= */ 
    /* Ref: https://highlightjs.org/usage/ */     
    $('pre code').each(function(i, block) {
	    hljs.highlightBlock(block);
	 });

	 const queryString = window.location.search;
	 const urlParams = new URLSearchParams(queryString);
	 const postId = urlParams.get('id');
	 
	 const postUrl = "https://localhost:7213/api/Post";


	 function getPost(postId) {
		fetch(`${postUrl}/${postId}`)
			.then((response) => response.json()) //=> arrow function
			.then((data) => {
				let contentElement = document.getElementById("Content");
				contentElement.innerHTML = data.content;
			})
			.catch((error) => console.error("Unable to get post.", error));
		}

		getPost(postId);
	});