<template>

    <section class="row d-flex justify-content-center">
        <div>
            <!-- <p class="mb-0 text-center">Views:{{ keep.views }} Kept:{{ keep.kept }}</p> -->
            <p>render check</p>
            <h2 class="text-center">{{ vault.name }}</h2>


            <RouterLink :to="{ name: 'VaultDetails', params: { vaultId: vault.id } }">
                <div>


                    <img :src="vault.img" :alt="vault.name" class="img">

                </div>
            </RouterLink>


            <p>{{ vault.description }}</p>
        </div>
    </section>


</template>


<script>
import { Vault } from '../models/Vault';

export default {
    props: {
        vault: { type: Vault, required: true }
    },
    setup(props) {
        return {
            // vaults: computed(() => AppState.accountVaults)
        }
    }
}
</script>


<style lang="scss" scoped></style>