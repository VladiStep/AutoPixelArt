﻿using System.Collections.Generic;

namespace MainLibrary
{
    public static class Palette
    {
		// { "", new byte[] {  } }
		public static readonly Dictionary<string, byte[]> v1_8 = new()
		{
			{ "coal_block", new byte[] { 18, 18, 18 } },
			{ "obsidian", new byte[] { 20, 18, 29 } },
			{ "black_wool", new byte[] { 25, 22, 22 } },
			{ "black_terracotta", new byte[] { 37, 22, 16 } },
			{ "lapis_block", new byte[] { 38, 67, 137 } },
			{ "nether_bricks", new byte[] { 44, 22, 26 } },
			{ "spruce_log", new byte[] { 45, 28, 12 } },
			{ "blue_wool", new byte[] { 46, 56, 141 } },
			{ "cyan_wool", new byte[] { 46, 110, 137 } },
			{ "dark_oak_log", new byte[] { 52, 40, 23 } },
			{ "green_wool", new byte[] { 53, 70, 27 } },
			{ "gray_terracotta", new byte[] { 57, 42, 35 } },
			{ "dark_prismarine", new byte[] { 59, 87, 75 } },
			{ "dark_oak_planks", new byte[] { 61, 39, 18 } },
			{ "gray_wool", new byte[] { 64, 64, 64 } },
			{ "lime_wool", new byte[] { 65, 174, 56 } },
			{ "blue_terracotta", new byte[] { 74, 59, 91 } },
			{ "green_terracotta", new byte[] { 76, 83, 42 } },
			{ "brown_terracotta", new byte[] { 77, 51, 35 } },
			{ "brown_wool", new byte[] { 79, 50, 31 } },
			{ "emerald_block", new byte[] { 81, 217, 117 } },
			{ "bedrock", new byte[] { 83, 83, 83 } },
			{ "cyan_terracotta", new byte[] { 86, 91, 91 } },
			{ "jungle_log", new byte[] { 87, 67, 26 } },
			{ "diamond_block", new byte[] { 97, 219, 213 } },
			{ "prismarine_bricks", new byte[] { 99, 160, 143 } },
			{ "oak_log", new byte[] { 102, 81, 49 } },
			{ "spruce_planks", new byte[] { 103, 77, 46 } },
			{ "lime_terracotta", new byte[] { 103, 117, 52 } },
			{ "mossy_cobblestone", new byte[] { 103, 121, 103 } },
			{ "acacia_log", new byte[] { 105, 99, 89 } },
			{ "light_blue_wool", new byte[] { 106, 138, 201 } },
			{ "netherrack", new byte[] { 111, 54, 52 } },
			{ "light_blue_terracotta", new byte[] { 113, 108, 137 } },
			{ "mossy_stone_bricks", new byte[] { 114, 119, 106 } },
			{ "purple_terracotta", new byte[] { 118, 70, 86 } },
			{ "cracked_stone_bricks", new byte[] { 118, 118, 118 } },
			{ "cobblestone", new byte[] { 122, 122, 122 } },
			{ "stone_bricks", new byte[] { 122, 122, 122 } },
			{ "stone", new byte[] { 125, 125, 125 } },
			{ "purple_wool", new byte[] { 126, 61, 181 } },
			{ "gravel", new byte[] { 126, 124, 122 } },
			{ "andesite", new byte[] { 130, 131, 131 } },
			{ "polished_andesite", new byte[] { 133, 133, 134 } },
			{ "dirt", new byte[] { 134, 96, 67 } },
			{ "light_gray_terracotta", new byte[] { 135, 106, 97 } },
			{ "melon", new byte[] { 141, 145, 36 } },
			{ "red_terracotta", new byte[] { 143, 61, 46 } },
			{ "glowstone", new byte[] { 143, 118, 69 } },
			{ "bricks", new byte[] { 146, 99, 86 } },
			{ "magenta_terracotta", new byte[] { 149, 88, 108 } },
			{ "red_wool", new byte[] { 150, 52, 48 } },
			{ "terracotta", new byte[] { 150, 92, 66 } },
			{ "granite", new byte[] { 153, 113, 98 } },
			{ "jungle_planks", new byte[] { 154, 110, 77 } },
			{ "light_gray_wool", new byte[] { 154, 161, 161 } },
			{ "oak_planks", new byte[] { 156, 127, 78 } },
			{ "clay", new byte[] { 158, 164, 176 } },
			{ "polished_granite", new byte[] { 159, 114, 98 } },
			{ "pink_terracotta", new byte[] { 161, 78, 78 } },
			{ "orange_terracotta", new byte[] { 161, 83, 37 } },
			{ "red_sandstone", new byte[] { 165, 84, 29 } },
			{ "red_sand", new byte[] { 169, 88, 33 } },
			{ "acacia_planks", new byte[] { 169, 91, 51 } },
			{ "redstone_block", new byte[] { 171, 27, 9 } },
			{ "yellow_wool", new byte[] { 177, 166, 39 } },
			{ "magenta_wool", new byte[] { 179, 80, 188 } },
			{ "diorite", new byte[] { 179, 179, 182 } },
			{ "polished_diorite", new byte[] { 183, 183, 185 } },
			{ "yellow_terracotta", new byte[] { 186, 133, 35 } },
			{ "sponge", new byte[] { 194, 195, 84 } },
			{ "birch_planks", new byte[] { 195, 179, 123 } },
			{ "pink_wool", new byte[] { 208, 132, 153 } },
			{ "white_terracotta", new byte[] { 209, 178, 161 } },
			{ "sandstone", new byte[] { 216, 209, 157 } },
			{ "orange_wool", new byte[] { 219, 125, 62 } },
			{ "sand", new byte[] { 219, 211, 160 } },
			{ "iron_block", new byte[] { 219, 219, 219 } },
			{ "white_wool", new byte[] { 221, 221, 221 } },
			{ "end_stone", new byte[] { 221, 223, 165 } },
			{ "quartz_block", new byte[] { 236, 233, 226 } },
			{ "gold_block", new byte[] { 249, 236, 78 } }
		};
		public static readonly Dictionary<string, byte[]> v1_9 = new()
		{
			{ "purpur_block", new byte[] { 166, 121, 166 } },
			{ "end_stone_bricks", new byte[] { 225, 230, 170 } }
		};
		public static readonly Dictionary<string, byte[]> v1_10 = new()
		{
			{ "red_nether_bricks", new byte[] { 68, 4, 6 } },
			{ "nether_wart_block", new byte[] { 117, 6, 7 } },
			{ "bone_block", new byte[] { 224, 220, 200 } }

		};
		public static readonly Dictionary<string, byte[]> v1_12 = new()
		{
			{ "black_concrete", new byte[] { 8, 10, 15 } },
			{ "cyan_concrete", new byte[] { 21, 119, 136 } },
			{ "black_concrete_powder", new byte[] { 25, 26, 31 } },
			{ "light_blue_concrete", new byte[] { 35, 137, 198 } },
			{ "cyan_concrete_powder", new byte[] { 36, 147, 157 } },
			{ "blue_concrete", new byte[] { 44, 46, 143 } },
			{ "gray_concrete", new byte[] { 54, 57, 61 } },
			{ "blue_concrete_powder", new byte[] { 70, 73, 166 } },
			{ "green_concrete", new byte[] { 73, 91, 36 } },
			{ "light_blue_concrete_powder", new byte[] { 74, 180, 213 } },
			{ "gray_concrete_powder", new byte[] { 76, 81, 84 } },
			{ "lime_concrete", new byte[] { 94, 168, 24 } },
			{ "brown_concrete", new byte[] { 96, 59, 31 } },
			{ "green_concrete_powder", new byte[] { 97, 119, 44 } },
			{ "purple_concrete", new byte[] { 100, 31, 156 } },
			{ "brown_concrete_powder", new byte[] { 125, 84, 53 } },
			{ "light_gray_concrete", new byte[] { 125, 125, 115 } },
			{ "lime_concrete_powder", new byte[] { 125, 189, 41 } },
			{ "purple_concrete_powder", new byte[] { 131, 55, 177 } },
			{ "red_concrete", new byte[] { 142, 32, 32 } },
			{ "light_gray_concrete_powder", new byte[] { 154, 154, 148 } },
			{ "red_concrete_powder", new byte[] { 168, 54, 50 } },
			{ "magenta_concrete", new byte[] { 169, 48, 159 } },
			{ "magenta_concrete_powder", new byte[] { 192, 83, 184 } },
			{ "white_concrete", new byte[] { 207, 213, 214 } },
			{ "pink_concrete", new byte[] { 213, 101, 142 } },
			{ "orange_concrete", new byte[] { 224, 97, 0 } },
			{ "white_concrete_powder", new byte[] { 225, 227, 227 } },
			{ "orange_concrete_powder", new byte[] { 227, 131, 31 } },
			{ "pink_concrete_powder", new byte[] { 228, 153, 181 } },
			{ "yellow_concrete_powder", new byte[] { 232, 199, 54 } },
			{ "yellow_concrete", new byte[] { 240, 175, 21 } }
		};
		public static readonly Dictionary<string, byte[]> v1_13 = new()
		{
			{ "stripped_dark_oak_log", new byte[] { 96, 76, 49 } },
			{ "stripped_spruce_log", new byte[] { 115, 89, 52 } },
			{ "dead_brain_coral_block", new byte[] { 124, 117, 114 } },
			{ "dead_tube_coral_block", new byte[] { 130, 123, 119 } },
			{ "dead_fire_coral_block", new byte[] { 131, 123, 119 } },
			{ "dead_horn_coral_block", new byte[] { 133, 126, 122 } },
			{ "brown_mushroom_block", new byte[] { 141, 106, 83 } },
			{ "smooth_stone", new byte[] { 158, 158, 158 } },
			{ "smooth_red_sandstone", new byte[] { 166, 85, 29 } },
			{ "stripped_jungle_log", new byte[] { 171, 132, 84 } },
			{ "stripped_acacia_log", new byte[] { 174, 92, 59 } },
			{ "stripped_oak_log", new byte[] { 177, 144, 86 } },
			{ "stripped_birch_log", new byte[] { 196, 176, 118 } },
			{ "mushroom_stem", new byte[] { 207, 204, 194 } },
			{ "smooth_sandstone", new byte[] { 218, 210, 158 } }
		};
		public static readonly Dictionary<string, byte[]> v1_14 = new()
		{
			{ "black_concrete", new byte[] { 8, 10, 15 } },
			{ "obsidian", new byte[] { 15, 10, 24 } },
			{ "coal_block", new byte[] { 16, 15, 15 } },
			{ "black_wool", new byte[] { 20, 21, 25 } },
			{ "cyan_concrete", new byte[] { 21, 119, 136 } },
			{ "cyan_wool", new byte[] { 21, 137, 145 } },
			{ "black_concrete_powder", new byte[] { 25, 26, 31 } },
			{ "lapis_block", new byte[] { 30, 67, 140 } },
			{ "light_blue_concrete", new byte[] { 35, 137, 198 } },
			{ "cyan_concrete_powder", new byte[] { 36, 147, 157 } },
			{ "black_terracotta", new byte[] { 37, 22, 16 } },
			{ "emerald_block", new byte[] { 42, 203, 87 } },
			{ "nether_bricks", new byte[] { 44, 21, 26 } },
			{ "blue_concrete", new byte[] { 44, 46, 143 } },
			{ "dark_prismarine", new byte[] { 51, 91, 75 } },
			{ "blue_wool", new byte[] { 53, 57, 157 } },
			{ "gray_concrete", new byte[] { 54, 57, 61 } },
			{ "gray_terracotta", new byte[] { 57, 42, 35 } },
			{ "spruce_log", new byte[] { 58, 37, 16 } },
			{ "light_blue_wool", new byte[] { 58, 175, 217 } },
			{ "dark_oak_log", new byte[] { 60, 46, 26 } },
			{ "gray_wool", new byte[] { 62, 68, 71 } },
			{ "dark_oak_planks", new byte[] { 66, 43, 20 } },
			{ "red_nether_bricks", new byte[] { 69, 7, 9 } },
			{ "blue_concrete_powder", new byte[] { 70, 73, 166 } },
			{ "green_concrete", new byte[] { 73, 91, 36 } },
			{ "blue_terracotta", new byte[] { 74, 59, 91 } },
			{ "light_blue_concrete_powder", new byte[] { 74, 180, 213 } },
			{ "gray_concrete_powder", new byte[] { 76, 81, 84 } },
			{ "green_terracotta", new byte[] { 76, 83, 42 } },
			{ "brown_terracotta", new byte[] { 77, 51, 35 } },
			{ "green_wool", new byte[] { 84, 109, 27 } },
			{ "jungle_log", new byte[] { 85, 67, 25 } },
			{ "bedrock", new byte[] { 85, 85, 85 } },
			{ "cyan_terracotta", new byte[] { 86, 91, 91 } },
			{ "lime_concrete", new byte[] { 94, 168, 24 } },
			{ "brown_concrete", new byte[] { 96, 59, 31 } },
			{ "stripped_dark_oak_log", new byte[] { 96, 76, 49 } },
			{ "netherrack", new byte[] { 97, 38, 38 } },
			{ "green_concrete_powder", new byte[] { 97, 119, 44 } },
			{ "diamond_block", new byte[] { 98, 237, 228 } },
			{ "prismarine_bricks", new byte[] { 99, 171, 158 } },
			{ "purple_concrete", new byte[] { 100, 31, 156 } },
			{ "acacia_log", new byte[] { 103, 96, 86 } },
			{ "lime_terracotta", new byte[] { 103, 117, 52 } },
			{ "oak_log", new byte[] { 109, 85, 50 } },
			{ "mossy_cobblestone", new byte[] { 110, 118, 94 } },
			{ "lime_wool", new byte[] { 112, 185, 25 } },
			{ "light_blue_terracotta", new byte[] { 113, 108, 137 } },
			{ "nether_wart_block", new byte[] { 114, 3, 2 } },
			{ "brown_wool", new byte[] { 114, 71, 40 } },
			{ "spruce_planks", new byte[] { 114, 84, 48 } },
			{ "melon", new byte[] { 114, 146, 30 } },
			{ "stripped_spruce_log", new byte[] { 115, 89, 52 } },
			{ "mossy_stone_bricks", new byte[] { 115, 121, 105 } },
			{ "purple_terracotta", new byte[] { 118, 70, 86 } },
			{ "cracked_stone_bricks", new byte[] { 118, 117, 118 } },
			{ "purple_wool", new byte[] { 121, 42, 172 } },
			{ "stone_bricks", new byte[] { 122, 121, 122 } },
			{ "dead_brain_coral_block", new byte[] { 124, 117, 114 } },
			{ "brown_concrete_powder", new byte[] { 125, 84, 53 } },
			{ "light_gray_concrete", new byte[] { 125, 125, 115 } },
			{ "stone", new byte[] { 125, 125, 125 } },
			{ "lime_concrete_powder", new byte[] { 125, 189, 41 } },
			{ "cobblestone", new byte[] { 127, 127, 127 } },
			{ "dead_tube_coral_block", new byte[] { 130, 123, 119 } },
			{ "purple_concrete_powder", new byte[] { 131, 55, 177 } },
			{ "dead_fire_coral_block", new byte[] { 131, 123, 119 } },
			{ "gravel", new byte[] { 131, 127, 126 } },
			{ "polished_andesite", new byte[] { 132, 134, 133 } },
			{ "dead_horn_coral_block", new byte[] { 133, 126, 122 } },
			{ "dirt", new byte[] { 134, 96, 67 } },
			{ "light_gray_terracotta", new byte[] { 135, 106, 97 } },
			{ "andesite", new byte[] { 136, 136, 136 } },
			{ "red_concrete", new byte[] { 142, 32, 32 } },
			{ "light_gray_wool", new byte[] { 142, 142, 134 } },
			{ "red_terracotta", new byte[] { 143, 61, 46 } },
			{ "magenta_terracotta", new byte[] { 149, 88, 108 } },
			{ "granite", new byte[] { 149, 103, 85 } },
			{ "brown_mushroom_block", new byte[] { 149, 111, 81 } },
			{ "bricks", new byte[] { 150, 97, 83 } },
			{ "terracotta", new byte[] { 152, 94, 67 } },
			{ "polished_granite", new byte[] { 154, 106, 89 } },
			{ "light_gray_concrete_powder", new byte[] { 154, 154, 148 } },
			{ "smooth_stone", new byte[] { 158, 158, 158 } },
			{ "red_wool", new byte[] { 160, 39, 34 } },
			{ "jungle_planks", new byte[] { 160, 115, 80 } },
			{ "clay", new byte[] { 160, 166, 179 } },
			{ "pink_terracotta", new byte[] { 161, 78, 78 } },
			{ "orange_terracotta", new byte[] { 161, 83, 37 } },
			{ "oak_planks", new byte[] { 162, 130, 78 } },
			{ "smooth_red_sandstone", new byte[] { 166, 85, 29 } },
			{ "red_concrete_powder", new byte[] { 168, 54, 50 } },
			{ "acacia_planks", new byte[] { 168, 90, 50 } },
			{ "magenta_concrete", new byte[] { 169, 48, 159 } },
			{ "purpur_block", new byte[] { 169, 125, 169 } },
			{ "glowstone", new byte[] { 171, 131, 84 } },
			{ "stripped_jungle_log", new byte[] { 171, 132, 84 } },
			{ "stripped_acacia_log", new byte[] { 174, 92, 59 } },
			{ "redstone_block", new byte[] { 175, 24, 5 } },
			{ "stripped_oak_log", new byte[] { 177, 144, 86 } },
			{ "red_sandstone", new byte[] { 186, 99, 29 } },
			{ "yellow_terracotta", new byte[] { 186, 133, 35 } },
			{ "diorite", new byte[] { 188, 188, 188 } },
			{ "magenta_wool", new byte[] { 189, 68, 179 } },
			{ "red_sand", new byte[] { 190, 102, 33 } },
			{ "magenta_concrete_powder", new byte[] { 192, 83, 184 } },
			{ "birch_planks", new byte[] { 192, 175, 121 } },
			{ "polished_diorite", new byte[] { 192, 193, 194 } },
			{ "sponge", new byte[] { 195, 192, 74 } },
			{ "stripped_birch_log", new byte[] { 196, 176, 118 } },
			{ "mushroom_stem", new byte[] { 203, 196, 185 } },
			{ "white_concrete", new byte[] { 207, 213, 214 } },
			{ "white_terracotta", new byte[] { 209, 178, 161 } },
			{ "pink_concrete", new byte[] { 213, 101, 142 } },
			{ "sandstone", new byte[] { 216, 203, 155 } },
			{ "smooth_sandstone", new byte[] { 218, 210, 158 } },
			{ "end_stone_bricks", new byte[] { 218, 224, 162 } },
			{ "sand", new byte[] { 219, 207, 163 } },
			{ "end_stone", new byte[] { 219, 222, 158 } },
			{ "iron_block", new byte[] { 220, 220, 220 } },
			{ "orange_concrete", new byte[] { 224, 97, 0 } },
			{ "white_concrete_powder", new byte[] { 225, 227, 227 } },
			{ "orange_concrete_powder", new byte[] { 227, 131, 31 } },
			{ "pink_concrete_powder", new byte[] { 228, 153, 181 } },
			{ "bone_block", new byte[] { 229, 225, 207 } },
			{ "yellow_concrete_powder", new byte[] { 232, 199, 54 } },
			{ "white_wool", new byte[] { 233, 236, 236 } },
			{ "quartz_block", new byte[] { 235, 229, 222 } },
			{ "pink_wool", new byte[] { 237, 141, 172 } },
			{ "orange_wool", new byte[] { 240, 118, 19 } },
			{ "yellow_concrete", new byte[] { 240, 175, 21 } },
			{ "gold_block", new byte[] { 246, 208, 61 } },
			{ "yellow_wool", new byte[] { 248, 197, 39 } }
		};
		public static readonly Dictionary<string, byte[]> v1_15 = new()
		{
			{ "honeycomb_block", new byte[] { 229, 148, 29 } }
		};
		public static readonly Dictionary<string, byte[]> v1_16 = new()
		{
			{ "warped_wart_block", new byte[] { 22, 119, 121 } },
			{ "crying_obsidian", new byte[] { 32, 10, 60 } },
			{ "respawn_anchor", new byte[] { 39, 23, 62 } },
			{ "blackstone", new byte[] { 42, 35, 40 } },
			{ "cracked_polished_blackstone_bricks", new byte[] { 43, 37, 43 } },
			{ "warped_planks", new byte[] { 43, 104, 99 } },
			{ "polished_blackstone_bricks", new byte[] { 46, 41, 48 } },
			{ "chiseled_nether_bricks", new byte[] { 47, 23, 28 } },
			{ "chiseled_polished_blackstone", new byte[] { 53, 48, 56 } },
			{ "polished_blackstone", new byte[] { 53, 48, 56 } },
			{ "gilded_blackstone", new byte[] { 56, 43, 38 } },
			{ "stripped_warped_stem", new byte[] { 57, 150, 147 } },
			{ "netherite_block", new byte[] { 66, 61, 63 } },
			{ "basalt", new byte[] { 73, 72, 77 } },
			{ "soul_soil", new byte[] { 75, 57, 46 } },
			{ "polished_basalt", new byte[] { 88, 88, 91 } },
			{ "ancient_debris", new byte[] { 95, 63, 55 } },
			{ "crimson_planks", new byte[] { 101, 48, 70 } },
			{ "nether_gold_ore", new byte[] { 115, 54, 42 } },
			{ "lodestone", new byte[] { 119, 119, 123 } },
			{ "stripped_crimson_stem", new byte[] { 137, 57, 90 } },
			{ "shroomlight", new byte[] { 240, 146, 70 } }
		};
	}
}