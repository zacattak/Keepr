<template>

    <div class="container-fluid">

        <section class="row">

            <div class="masonry">

                <div v-for="vault in vaults" :key="vault.id" class="">

                    <VaultComponent :vault="vault" />

                </div>

            </div>

        </section>

    </div>

</template>


<script>
import Pop from '../utils/Pop.js'
import { vaultsService } from '../services/VaultsService.js'
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js'
import VaultComponent from '../components/VaultComponent.vue';


export default {
    setup() {


        return {
            vaults: computed(() => AppState.accountVaults),

        }
    },
    components: { VaultComponent }
}
</script>


<style lang="scss" scoped>
.container-fluid {
    background-color: #dcdcdc;
    /* Cooler, more modern gray */
    min-height: 100vh;
    /* Ensure the background covers the full page height */
}

.card {
    border: 2px solid black;
    border-radius: 16px;
    box-shadow: 3px 3px 10px rgba(42, 6, 134, 0.31);
}

.masonry {
    column-count: 3;
    /* Set the number of columns */
    column-gap: 1rem;
    /* Adjust gap between columns */


}

.masonry>div {
    break-inside: avoid;
    /* Prevent the item from breaking between columns */
    margin-bottom: 1rem;
    /* Add some spacing between items */
}

/* For tablets (medium screens), show 2 columns */
@media (max-width: 768px) {
    .masonry {
        column-count: 2;
        /* Reduce to 2 columns */
    }
}

/* For mobile phones (small screens), show 1 column */
@media (max-width: 576px) {
    .masonry {
        column-count: 1;
        /* Reduce to 1 column */
    }
}
</style>