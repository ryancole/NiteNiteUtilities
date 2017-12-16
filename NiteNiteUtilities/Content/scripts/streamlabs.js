const socketToken =
  "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ0b2tlbiI6IkI4OTNFQzUxRjdDNTZBMzNGMzQ5IiwicmVhZF9vbmx5Ijp0cnVlLCJwcmV2ZW50X21hc3RlciI6dHJ1ZSwidHdpdGNoX2lkIjoiMTkxODExMTMifQ.4IIAKL23NWx3FGvL0no_56yEpYqrAZOqsMexkONqJ-A"; //Socket token from /socket/token end point

const streamlabs = io(`https://sockets.streamlabs.com?token=${socketToken}`);

let isShowingEvent = false;
const events = [];

setInterval(showNextEvent, 5000);

streamlabs.on("event", eventData => {
  if (eventData.for === "twitch_account") {
    switch (eventData.type) {
      case "follow":
        queueFollow(eventData);
        break;
    }
  }
});

function showNextEvent() {
  // if we're currently showing an event, we don't want to overlap
  if (isShowingEvent) {
    return;
  }

  // pop off the next event
  const event = events.shift();

  if (!event) {
    return;
  }

  // set us in the showing event state
  isShowingEvent = true;

  // handle the event based no its type
  switch (event.type) {
    case "follow":
      cycleLatestFollower(event);
      break;
  }
}

function queueFollow({ message }) {
  const { name } = message.shift();
  events.push({
    type: "follow",
    name: name
  });
}

function cycleLatestFollower({ name }) {
  const logo = document.getElementById("logo");
  const followerName = document.getElementById("follower-name");
  const followerContainer = document.getElementById("follower-container");

  // set the text value of our follower name element
  followerName.textContent = name;

  // make the follower element visible
  logo.classList.add("tossing");
  followerContainer.classList.add("slideLeft");

  // hide the follower element after a number of seconds pass
  setTimeout(() => {
    logo.classList.remove("tossing");
    followerContainer.classList.remove("slideLeft");

    isShowingEvent = false;
  }, 10000);
}
