<template>
<body>
  <section class="hero is-primary is-bold">
    <div class="hero-body">
      <div class="container">
        <h1 class="title">Lowfare train</h1>
      </div>
    </div>
  </section>
  <section></section>
  <SearchView />
</body>
</template>

<script>
import CalendarView from "./views/calendar/CalendarView";
import SearchView from "./views/search/SearchView"

export default {
  name: "app",
  components: {
    CalendarView,
    SearchView
  }
};
</script>

<style lang="scss">
@import "~bulma/sass/utilities/_all";

$primary: #0a9578;
$primary-invert: findColorInvert($primary);

$colors: (
  "white": (
    $white,
    $black
  ),
  "black": (
    $black,
    $white
  ),
  "light": (
    $light,
    $light-invert
  ),
  "dark": (
    $dark,
    $dark-invert
  ),
  "primary": (
    $primary,
    $primary-invert
  ),
  "info": (
    $info,
    $info-invert
  ),
  "success": (
    $success,
    $success-invert
  ),
  "warning": (
    $warning,
    $warning-invert
  ),
  "danger": (
    $danger,
    $danger-invert
  )
);

@import "~bulma";
@import "~buefy/src/scss/buefy";
</style>
