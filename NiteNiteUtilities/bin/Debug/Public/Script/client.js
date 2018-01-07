
const toastyElement = document.getElementById("toasty");

const followerElement = document.getElementById("follower");

const nameSlideDownElement = document.getElementById("nameSlideDown");
const nameSlideDownContainerElement = document.getElementById("nameSlideDownContainer");
const nameSlideDownParagraphElements = document.querySelectorAll("#nameSlideDownContainer p");

async function fetchNextFollower() {
	// we need to fetch the details of a most recent follower, if any
	const response = await fetch("http://localhost:8080/api/twitchfollower");

	// there may not be a recent follower to show
	if (response.status != 200) {
		return;
	}

	// now that we got a response, we need to convert it to json
	const json = await response.json();

	return json;
}

async function displayNextFollower() {
  const follower = await fetchNextFollower();
  if (follower) {
    beginAnimation(follower);
    toastyElement.play();
  }
}

function beginAnimation(follower) {
  nameSlideDownElement.textContent = follower.display_name;
  nameSlideDownContainerElement.classList.remove("invisible");
  nameSlideDownParagraphElements.forEach(e => e.classList.add("animated"));
}

function handleAnimationEnd(event) {
  if (event.animationName == "slideUpRight") {
    const name = event.target.textContent;
	followerElement.textContent = name;
	nameSlideDownContainerElement.classList.add("invisible");
	nameSlideDownParagraphElements.forEach(e => e.classList.remove("animated"));
	setTimeout(displayNextFollower, 1000);
  }
}

// register our event listeners
nameSlideDownElement.addEventListener("animationend", handleAnimationEnd, false);

// in seconds we want to start showing followers
setTimeout(displayNextFollower, 2000);