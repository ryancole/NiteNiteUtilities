
const logoIcon = document.getElementById("logo-icon");
const twitchFollowReminder = document.getElementById("twitch-follow-reminder");

const toastyElement = document.getElementById("toasty");

const logoSlideDownElement = document.getElementById("followerLogo");
const nameSlideDownElement = document.getElementById("followerName");
const nameSlideDownContainerElement = document.getElementById("followerContainer");

function beginTopRoller() {
  let currentIndex = 0;
  const possibleItems = [
    { el: logoIcon, duration: 5000 },
	{ el: twitchFollowReminder, duration: 5000 }
  ];
  function nextIndex() {
    if (currentIndex + 1 >= possibleItems.length) {
	  currentIndex = 0;
	} else {
	  currentIndex += 1;
	}
	return currentIndex;
  }
  function cycle() {
    // hide current item
	const current = possibleItems[currentIndex].el;
	current.classList.add("d-none");
	// show next item
	const next = possibleItems[nextIndex()];
	next.el.classList.remove("d-none");
	// queue up the next cycle
	setTimeout(cycle, next.duration);
  }
  setTimeout(cycle, 0);
}

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
  } else {
    setTimeout(displayNextFollower, 1000);
  }
}

function beginAnimation(follower) {
  // set text and background logo
  logoSlideDownElement.style.backgroundImage = `url('${follower.profile_image_url}')`;
  nameSlideDownElement.textContent = follower.display_name;

  // make follower visible
  nameSlideDownContainerElement.classList.remove("invisible");

  // begin animation
  nameSlideDownContainerElement.classList.add("new-follower-animation");
}

function handleAnimationEnd(event) {
  if (event.animationName == "slideUp") {
    // make follower invisible
	nameSlideDownContainerElement.classList.add("invisible");

	// end animation
	nameSlideDownContainerElement.classList.remove("new-follower-animation");

	// queue up next follower
	setTimeout(displayNextFollower, 1000);
  }
}

// register our event listeners
nameSlideDownContainerElement.addEventListener("animationend", handleAnimationEnd, false);

// in seconds we want to start showing followers
setTimeout(displayNextFollower, 2000);

// start our top roller
beginTopRoller();