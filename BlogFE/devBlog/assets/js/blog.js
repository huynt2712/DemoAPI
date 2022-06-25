$(document).ready(function() {
	
	/* ======= Highlight.js Plugin ======= */ 
    /* Ref: https://highlightjs.org/usage/ */     
    $('pre code').each(function(i, block) {
	    hljs.highlightBlock(block);
	 });

	 const postUrl = 'https://localhost:7213/api/Post';

	 function displayDetailPost()
	 {
		const queryString = window.location.search;
		const urlParams = new URLSearchParams(queryString);
		const postId = urlParams.get('id');

		let contentElement = document.getElementById("blog-post-content");
		if(!contentElement) return;

		let titleElement = document.getElementById('blog-post-title');
		if(!titleElement) return;

		fetch(`${postUrl}/${postId}`)
		.then((response) => response.json())
		.then((data) => {
			titleElement.textContent = data.title;
			contentElement.innerHTML = data.content;
		});
	 }

	 displayDetailPost();
});