<template>
    <nav class="bg-dark">
        <section class="row">
            <div class="col-4 text-center">

                <!-- <p class="my-3 text-info">placeholder</p> -->
                <div class="dropdown my-2">
                    <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton"
                        data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        Create
                    </button>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                        <a class="dropdown-item" href="#">Create Keep</a>
                        <a class="dropdown-item" href="#">Create Vault</a>
                        <!-- <a class="dropdown-item" href="#">Something else here</a> -->
                    </div>
                </div>


            </div>

            <div class="col-4 text-center">
                <!-- <h1>🕋</h1> -->
                <router-link class="" :to="{ name: 'Home' }">
                    <p class="my-3 text-info">Chamber of Keeps</p>
                </router-link>

            </div>

            <div class="col-4 text-center">
                <p class="my-2">
                    <Login />
                </p>
            </div>

        </section>

    </nav>
</template>


<script>
import Login from './Login.vue';
export default {
    setup() {
        return {}
    },

    components: { Login }
}
</script>


<style lang="scss" scoped></style>