
const toastyElement = document.getElementById("toasty");

const followerElement = document.getElementById("follower");
const followerContainerElement = document.getElementById("followerContainer");

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
	// first we gotta fetch a follower
	const follower = await fetchNextFollower();

	if (follower) {
		// set the element text to the follower name
		followerElement.textContent = follower.display_name;

		// add the animation class
		followerContainerElement.classList.remove("invisible");

		// play the toasty sound
		toastyElement.play();
	}

	// in ten seconds we want to show the next follower
	setTimeout(displayNextFollower, 10000);
}

// in five seconds we want to start showing followers
setTimeout(displayNextFollower, 5000);